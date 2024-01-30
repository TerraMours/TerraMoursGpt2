<template>
  <n-drawer :show="modalVisible" display-directive="show" :width="330" @mask-click="closeModal">
    <n-drawer-content title="菜单配置" :native-scrollbar="false">
      <n-space class="w-full pt-24px pb-12px" :size="24" justify="end">
        <n-button size="small" type="primary" class="mr-auto" @click="getMenuData">
          <icon-mdi-refresh class="mr-4px text-16px" :class="{ 'animate-spin': loading }" />
          刷新
        </n-button>
      </n-space>
      <n-tree v-model:checked-keys="checkIds" class="h-full" block-line cascade checkable :data="menuData" />
      <n-space class="w-full pt-16px" :size="24" justify="end">
        <n-button class="w-72px" @click="closeModal">取消</n-button>
        <n-button class="w-72px" type="primary" @click="handleSubmit">确定</n-button>
      </n-space>
    </n-drawer-content>
  </n-drawer>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue';
import type { TreeOption } from 'naive-ui';
import { NTree } from 'naive-ui';
import { fetchMenuTreeList, fetchAddMenuToRole, fetchGetRoleMenusIds } from '@/service';
import { useLoading } from '@/hooks';
const { loading, startLoading, endLoading } = useLoading(false);
const menuData = ref<TreeOption[]>([]);
const checkIds = ref<number[]>([]);
export interface Props {
  /** 弹窗可见性 */
  editVisible: boolean;
  /** 编辑的表格行数据 */
  editData?: ApiUserManagement.Role | null;
  roleId: number | null;
}
interface Emits {
  (e: 'update:editVisible', editVisible: boolean): void;
}
const props = withDefaults(defineProps<Props>(), {
  editData: null,
  roleId: null
});
const emit = defineEmits<Emits>();

const modalVisible = computed({
  get() {
    if (props.editVisible === true) {
      init();
    }
    return props.editVisible;
  },
  set(editVisible) {
    emit('update:editVisible', editVisible);
  }
});
const closeModal = () => {
  modalVisible.value = false;
};
async function handleSubmit() {
  const { data } = await fetchAddMenuToRole(props.roleId, checkIds.value);
  if (data) {
    window.$message?.success('设置成功!');
    closeModal();
  }
}
function setMenuData(data: TreeOption[]) {
  menuData.value = data;
}
async function getMenuData() {
  startLoading();
  const { data } = await fetchMenuTreeList(props.roleId);
  if (data) {
    setTimeout(() => {
      setMenuData(data);
      getMenuIds();
      endLoading();
    }, 1000);
  }
}
async function getMenuIds() {
  startLoading();
  const { data } = await fetchGetRoleMenusIds(props.roleId);
  if (data) {
    checkIds.value = data;
  }
}

function init() {
  getMenuData();
}

// 初始化
init();
</script>

<style scoped></style>
