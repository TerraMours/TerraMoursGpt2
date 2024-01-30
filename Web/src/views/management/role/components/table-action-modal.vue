<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-form ref="formRef" label-placement="left" :label-width="80" :model="formModel">
      <n-grid :cols="12" :x-gap="18">
        <n-form-item-grid-item :span="12" label="角色名称" path="roleName">
          <n-input v-model:value="formModel.roleName" />
        </n-form-item-grid-item>
      </n-grid>
      <n-grid>
        <n-form-item-grid-item :span="12" label="系统管理员" path="isHome">
          <n-switch v-model:value="formModel.isAdmin">
            <template #checked>是</template>
            <template #unchecked>否</template>
          </n-switch>
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="默认新增用户角色" path="isHome">
          <n-switch v-model:value="formModel.isNewUser">
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
import { fetchUpdateRole, fetchAddRole } from '@/service';

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
  editData?: UserManagement.Role | null;
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
    add: '添加角色',
    edit: '编辑角色'
  };
  return titles[props.type];
});

const formRef = ref<HTMLElement & FormInst>();

type FormModel = Pick<UserManagement.Role, 'roleId' | 'roleName' | 'isAdmin' | 'isNewUser'>;

const formModel = reactive<FormModel>(createDefaultFormModel());

function createDefaultFormModel(): FormModel {
  return {
    roleId: 0,
    roleName: '',
    isAdmin: false,
    isNewUser: false
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
      const { data } = await fetchAddRole(formModel.roleName, formModel.isAdmin, formModel.isNewUser);
      if (data) {
        window.$message?.success('新增成功!');
        emit('updateDataTable');
        closeModal();
      }
    },
    edit: async () => {
      if (props.editData) {
        const { data } = await fetchUpdateRole(
          formModel.roleId,
          formModel.roleName,
          formModel.isAdmin,
          formModel.isNewUser
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
</script>

<style scoped></style>
