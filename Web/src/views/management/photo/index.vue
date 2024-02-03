<template>
  <div class="h-full overflow-y-auto">
    <n-card  :bordered="false" class="rounded-16px shadow-sm ">
    <n-row>
      <n-col :span="12">
        <n-statistic label="当前队列等待任务数" :value="waitingCount" />
      </n-col>
    </n-row>
    <n-tabs v-model:value="selectedTab" type="segment" @update:value="history">
      <n-tab-pane name="chap1" tab="我的绘画" />
      <n-tab-pane name="chap2" tab="绘画广场" />
    </n-tabs>
    <div
        id="image-scroll-container"
        style="
		      overflow: auto;
		      display: flex;
		      min-height: 500px;
		      flex-wrap: wrap;
		      gap: 8px;
		    "
    >
      <n-card v-for="(imageUrl, index) in imgUrl" :key="index" shadow="hover" style="margin-bottom: 10px;max-width: 300px;" hoverable>
        <template #cover>
          <n-image
              width="512"
              height="512"
              lazy
              :src="imageUrl.imagUrl"
              :intersection-observer-options="{
              root: '#image-scroll-container',
            }"
          >
            <template #placeholder>
              <div
                  style="
						width: 100px;
						height: 100px;
						display: flex;
						align-items: center;
						justify-content: center;
						background-color: #0001;
					"
              >
                Loading
              </div>
            </template>
          </n-image>
        </template>
        <div style="display: flex; justify-content:space-between; margin-top: 16px;">
          <n-switch v-if="imageUrl.isPublic !== null" v-model:value="imageUrl.isPublic" @update:value="PublicChange(imageUrl.imageRecordId, imageUrl.isPublic)">
            <template #checked>
              公开
            </template>
            <template #unchecked>
              私有
            </template>
          </n-switch>
          <div style="display: none; justify-content: end;">
            <n-badge v-model:value="imageUrl.likeCount" style="margin-right: 8px;">
              <n-avatar>
                <SvgIcon icon="ri:thumb-up-fill" class="text-2xl cursor-pointer" :color="{ 'yellow': imageUrl.islike, '': !imageUrl.islike }" @click="changeBoolean(imageUrl)" />
              </n-avatar>
            </n-badge>
            <n-badge v-model:value="imageUrl.likeCount" style="margin-right: 8px;">
              <n-avatar>
                <SvgIcon icon="ri:thumb-down-fill" class="text-2xl cursor-pointer" color="" />
              </n-avatar>
            </n-badge>
            <n-badge value="1">
              <n-avatar>
                <SvgIcon icon="ri:star-fill" class="text-2xl cursor-pointer" />
              </n-avatar>
            </n-badge>
          </div>
        </div>
      </n-card>
    </div>
    <div v-if="totalPage > 0" class="pagination-wrap w-full" style="display: flex; justify-content: center; margin-top: 8px;">
      <n-pagination
          :page="page"
          :page-slot="5"
          :page-count="totalPage"
          @update:page="updatePage"
      />
    </div>
      <footer :class="footerClass">
        <div class="flex items-center justify-between space-x-2">
          <NPopselect
              v-model:value="formData.model" :options="modelOptions" trigger="click"
              :on-update:value="(value) => { formData.model = value;formData.Count = 1 }"
          >
            <NButton>{{ modelOptions.find(i => i.value === formData.model)?.label || '二次元' }}</NButton>
          </NPopselect>
          <NInput
              ref="inputRef"
              v-model:value="formData.Prompt"
              type="textarea"
              placeholder="请输入图片描述词"
              :autosize="{ minRows: 1, maxRows: isMobile ? 4 : 8 }"
              show-count
          />
          <NButton type="primary"  @click="submit">
            <template #icon>
            <span class="dark:text-black">
              <SvgIcon :icon="svgIcon" />
            </span>
            </template>
          </NButton>
        </div>
      </footer>
    </n-card>
  </div>
</template>
<script setup lang="tsx">
import {computed,onBeforeMount, onMounted, reactive, ref} from "vue";
import {useMessage} from "naive-ui";
import {apiUrl, GenerateGraph, MyImageList, ShareImage, ShareImageList} from "@/service";
import {useSignalR} from "@/service/api/useSignalR";
import {useBasicLayout} from "@/composables";
const { isMobile } = useBasicLayout();
const footerClass = computed(() => {
  if (isMobile)
    return ['sticky', 'left-0', 'bottom-0', 'right-0', 'p-2', 'pr-3', 'overflow-hidden']

  return ['p-4']
});
const svgIcon = ref('ri:send-plane-fill');
const modelOptions: Array<{ label: string; value: string }> = [
  { label: 'SD[二次元]', value: 'SD2D' },
  { label: 'SD[真人]', value: 'SD3D' },
  { label: '百度[Stable-Diffusion-XL]', value: 'Stable-Diffusion-XL' },
  { label: 'CHATGPT', value: 'gpt-3.5-turbo' },
]
const selectedTab = ref('chap1')
const page = ref(1)
const pageSize = ref(10)
const totalPage = ref(0)

const ms = useMessage()
// signalR
const { waitingCount, connection, imgUrl, start } = useSignalR(`${apiUrl}/graphhub`)

const formData = reactive<ApiGptManagement.SubmitDTO>({
  Prompt: '',
  Count: 1,
  Size: 512,
  negativePrompt: '',
  model: 'SD2D',
  connectionId: null,
})

const submit = async () => {
  if (!connection.value) {
    ms.warning('页面已失效，请刷新页面！')
    return
  }
  if (formData.Prompt.length == 0) {
    ms.warning('请填写描述词！')
    return
  }
  formData.connectionId = connection.value?.connectionId
  if (formData.model != 'SD2D' && formData.model != 'SD3D')
    formData.Size = 1024

  const { data } = await GenerateGraph(formData)
  if(data != null){
  ms.success(data)
  formData.Prompt = ''
  }
  // console.log('提交的参数：', formData) // 在控制台输出提交的参数
}

const history = async (tabName: string) => {
  let response
  switch (tabName) {
    case 'chap1':
      response = await MyImageList(null, 1, 10)
      break
    case 'chap2':
    default:
      response = await ShareImageList(null, 1, 10)
      break
  }
  // 设置请求头
  const { data } = response
  if (data != null && data.items != null) {
    imgUrl.value = data.items
    page.value = 1
    totalPage.value = Math.ceil(data.total / pageSize.value)
  }
}

const PublicChange = async (imageRecordId: number, isPublic: boolean) => {
  const { data } = await ShareImage(imageRecordId, isPublic)
  if (data)
    ms.success('操作成功')
}

const loadPosts = async () => {
  let response
  switch (selectedTab.value) {
    case 'chap1':
      response = await MyImageList(null, page.value, 10)
      break
    case 'chap2':
    default:
      response = await ShareImageList(null, page.value, 10)
      break
  }
  // 设置请求头
  const { data } = response
  if (data !=null && data.items != null) {
    imgUrl.value = data.items
    totalPage.value = Math.ceil(data.total / pageSize.value)
  }
}
const updatePage = (p: number) => {
  page.value = p
  loadPosts()
}

const changeBoolean = (imageUrl: any) => {
  if (imageUrl.islike == null)
    imageUrl.islike = true

  else
    imageUrl.islike = !imageUrl.islike
}
onMounted(() => {
  loadPosts()
})
onBeforeMount(() => {
  // 在组件加载时调用 start 方法
  start()
})
</script>
