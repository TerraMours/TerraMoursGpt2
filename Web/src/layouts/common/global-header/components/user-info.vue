<template>
  <n-modal v-model:show="show" :auto-focus="false" preset="card" style="width: 95%; max-width: 640px">
    <n-spin :show="loading">
      <div class="p-4 space-y-5 min-h-[200px]">
        <div class="space-y-6">
          <div class="flex items-center space-x-4">
            <span class="flex-shrink-0 w-[100px]">{{ t('message.setting.name') }}</span>
            <div class="w-[200px]">
              <NInput v-model:value="computedConfig.userName" placeholder="" />
            </div>
          </div>
          <div class="flex items-center space-x-4">
            <span class="flex-shrink-0 w-[100px]">{{ t('message.setting.avatarLink') }}</span>
            <div class="w-[200px]">
              <NUpload
                v-model:file-list="fileList"
                :action="uploadUrl"
                :headers="{ Authorization: `Bearer ${localStg.get('token')}` }"
                list-type="image-card"
                :max="1"
                accept="image/png, image/jpeg"
                @finish="handleFinish"
              />
            </div>
          </div>
          <div class="flex items-center space-x-4">
            <span class="flex-shrink-0 w-[100px]">{{ t('message.setting.vipLevel') }}</span>
            <div v-if="computedConfig.vipExpireTime" class="flex-1">
              <NGradientText gradient="linear-gradient(90deg, red 0%, green 50%, blue 100%)">
                尊贵的VIP用户
              </NGradientText>
            </div>
            <NButton size="small" color="#8a2be2" @click="routeToGoods">
              {{ t('message.setting.tobeVip') }}
            </NButton>
          </div>
          <div v-if="computedConfig.vipExpireTime" class="flex items-center space-x-4">
            <span class="flex-shrink-0 w-[100px]">{{ t('message.setting.vipExpireTime') }}</span>
            <div class="flex-1">
              <NTime :time="new Date(computedConfig.vipExpireTime)" type="date" />
            </div>
          </div>
          <div class="flex items-center space-x-4">
            <span class="flex-shrink-0 w-[100px]">{{ t('message.setting.balance') }}</span>
            <div class="flex-1">
              <NStatistic tabular-nums>
                <NNumberAnimation :from="0.0" :to="computedConfig.balance" :precision="3" />
                <template #prefix>￥</template>
              </NStatistic>
            </div>
          </div>

          <div class="flex items-center space-x-4">
            <div class="flex-1" />
            <NButton size="small" @click="UpdateUser">
              {{ t('message.setting.save') }}
            </NButton>
          </div>
        </div>
      </div>
    </n-spin>
  </n-modal>
</template>
<script setup lang="tsx">
import { computed, onMounted, ref } from 'vue';
import type { UploadFileInfo } from 'naive-ui';
import { useMessage } from 'naive-ui';
import { routeName } from '@/router';
import { apiUrl, fetchUpdateUser, fetchUserInfo } from '@/service';
import { useRouterPush } from '@/composables';
import { localStg } from '@/utils';
import { t } from '@/locales';

interface Props {
  /** 弹窗显隐 */
  value: boolean;
}
const props = defineProps<Props>();
interface Emits {
  (e: 'update:value', val: boolean): void;
}

const emit = defineEmits<Emits>();
const show = computed({
  get() {
    return props.value;
  },
  set(val: boolean) {
    emit('update:value', val);
  }
});

const config = ref<ApiAuth.UserInfo>();
const { routerPush } = useRouterPush();
const ms = useMessage();
const computedConfig = computed(() => config.value || ({} as ApiAuth.UserInfo));
const loading = ref(false);
const fileList = ref<UploadFileInfo[]>();
const uploadUrl = `${apiUrl}/api/v1/Product/UploadProductImage`;
const handleFinish = ({ file, event }: { file: UploadFileInfo; event?: ProgressEvent }) => {
  const res = JSON.parse((event?.target as XMLHttpRequest).response);
  file.url = `${apiUrl}${res.data}`;
  return file;
};
function routeToGoods() {
  show.value = false;
  routerPush({ name: routeName('management_goods') });
}
async function fetchUser() {
  try {
    loading.value = true;
    const { data } = await fetchUserInfo();
    config.value = data;
    if (data.headImageUrl !== null) {
      fileList.value = [
        {
          id: 'c',
          name: '我是自带url的图片.png',
          status: 'finished',
          url: data.headImageUrl
        }
      ];
    } else {
      fileList.value = [];
    }
  } finally {
    loading.value = false;
  }
}
async function UpdateUser() {
  if (config.value === null) {
    ms.error('获取用户失败');
    return;
  }
  if (fileList.value !== null && fileList.value![0] !== null) config.value.headImageUrl = fileList.value![0].url;
  const { data } = await fetchUpdateUser(config.value.userId, config.value.userName, config.value?.headImageUrl);
  if (data) {
    ms.success('保存成功');
    show.value = false;
  } else ms.error('保存失败');
}

onMounted(() => {
  fetchUser();
});
</script>
