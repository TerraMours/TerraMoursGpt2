<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-form ref="formRef" label-placement="left" :label-width="80" :model="formModel">
      <n-grid :cols="24" :x-gap="18">
        <n-form-item-grid-item :span="12" label="菜单名称" path="menuName">
          <n-input v-model:value="formModel.menuName" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="图标" path="icon">
          <n-input v-model:value="formModel.icon" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="父级菜单" path="parentId">
          <n-select v-model:value="formModel.parentId" :options="options" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="排序" path="orderNo">
          <n-input-number v-model:value="formModel.orderNo" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="链接" path="menuUrl">
          <n-input v-model:value="formModel.menuUrl" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="类型" path="menuName">
          <n-switch v-model:value="formModel.externalUrl">
            <template #checked>外链</template>
            <template #unchecked>路由</template>
          </n-switch>
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="可见" path="isShow">
          <n-switch v-model:value="formModel.isShow">
            <template #checked>可见</template>
            <template #unchecked>不可见</template>
          </n-switch>
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="首页" path="isHome">
          <n-switch v-model:value="formModel.isHome">
            <template #checked>是</template>
            <template #unchecked>否</template>
          </n-switch>
        </n-form-item-grid-item>
      </n-grid>
      <n-space class="w-full pt-16px" :size="24" justify="end">
        <n-button class="w-72px" @click="closeModal">取消</n-button>
        <n-button class="w-72px" type="primary" @click="handleSubmit">确定</n-button>
      </n-space>
    </n-form>
  </n-modal>
</template>

<script setup lang="ts">
import { ref, computed, reactive, watch } from 'vue';
import type { FormInst } from 'naive-ui';
import { fetchUpdateMenu, fetchAddMenu, fetchGetMenuSelect } from '@/service';
let options: { value: string | number; label: string }[] = [];
export interface Props {
  /** 弹窗可见性 */
  visible: boolean;
  /**
   * 弹窗类型
   * add: 新增
   * edit: 编辑
   */
  type?: 'add' | 'edit';
  /** 编辑的表格行数据 */
  editData?: UserManagement.Menu | null;
}

export type ModalType = NonNullable<Props['type']>;

defineOptions({ name: 'TableActionModal' });

const props = withDefaults(defineProps<Props>(), {
  type: 'add',
  editData: null
});

interface Emits {
  (e: 'update:visible', visible: boolean): void;
  (e: 'updateDataTable'): void;
}

const emit = defineEmits<Emits>();

const modalVisible = computed({
  get() {
    if (props.visible) {
      getMenuData();
    }
    return props.visible;
  },
  set(visible) {
    emit('update:visible', visible);
  }
});
const closeModal = () => {
  modalVisible.value = false;
};

const title = computed(() => {
  const titles: Record<ModalType, string> = {
    add: '添加菜单',
    edit: '编辑菜单'
  };
  return titles[props.type];
});

const formRef = ref<HTMLElement & FormInst>();

type FormModel = Pick<
  UserManagement.Menu,
  | 'menuId'
  | 'parentId'
  | 'menuName'
  | 'menuUrl'
  | 'icon'
  | 'description'
  | 'isHome'
  | 'externalUrl'
  | 'isShow'
  | 'remark'
  | 'orderNo'
>;

const formModel = reactive<FormModel>(createDefaultFormModel());

function createDefaultFormModel(): FormModel {
  return {
    menuId: 0,
    menuName: '',
    parentId: null,
    menuUrl: null,
    icon: null,
    description: null,
    isHome: false,
    externalUrl: false,
    isShow: true,
    remark: '',
    orderNo: 0
  };
}

function handleUpdateFormModel(model: Partial<FormModel>) {
  Object.assign(formModel, model);
}

function handleUpdateFormModelByModalType() {
  const handlers: Record<ModalType, () => void> = {
    add: () => {
      const defaultFormModel = createDefaultFormModel();
      handleUpdateFormModel(defaultFormModel);
    },
    edit: () => {
      if (props.editData) {
        handleUpdateFormModel(props.editData);
      }
    }
  };

  handlers[props.type]();
}

async function handleSubmit() {
  await formRef.value?.validate();
  const handlers: Record<ModalType, () => void> = {
    add: async () => {
      const { data } = await fetchAddMenu(
        formModel.parentId,
        formModel.menuName,
        formModel.menuUrl,
        formModel.icon,
        formModel.description,
        formModel.isHome,
        formModel.externalUrl,
        formModel.isShow,
        formModel.remark,
        formModel.orderNo
      );
      if (data) {
        window.$message?.success('新增成功!');
        emit('updateDataTable');
        closeModal();
      }
    },
    edit: async () => {
      if (props.editData) {
        const { data } = await fetchUpdateMenu(
          formModel.menuId,
          formModel.parentId,
          formModel.menuName,
          formModel.menuUrl,
          formModel.icon,
          formModel.description,
          formModel.isHome,
          formModel.externalUrl,
          formModel.isShow,
          formModel.remark,
          formModel.orderNo
        );
        if (data) {
          window.$message?.success('更新成功!');
          closeModal();
          emit('updateDataTable');
        }
      }
    }
  };

  handlers[props.type]();
}

watch(
  () => props.visible,
  newValue => {
    if (newValue) {
      handleUpdateFormModelByModalType();
    }
  }
);
async function getMenuData() {
  const { data } = await fetchGetMenuSelect(null, props.editData?.menuId ?? null);
  if (data) {
    setTimeout(() => {
      options = data.map(item => {
        return {
          value: item.key,
          label: item.value
        };
      });
    }, 1000);
  }
}
</script>

<style scoped></style>
