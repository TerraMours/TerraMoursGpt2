<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-form ref="formRef" label-placement="left" :label-width="80" :model="formModel" :rules="rules">
      <n-grid :cols="24" :x-gap="18">
        <n-form-item-grid-item :span="12" label="用户名" path="userName">
          <n-input v-model:value="formModel.userName" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="性别" path="gender">
          <n-radio-group v-model:value="formModel.gender">
            <n-radio v-for="item in genderOptions" :key="item.value" :value="item.value">{{ item.label }}</n-radio>
          </n-radio-group>
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="邮箱" path="userEmail">
          <n-input v-model:value="formModel.userEmail" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="手机号" path="userPhoneNum">
          <n-input v-model:value="formModel.userPhoneNum" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="设置角色" path="userPhoneNum">
          <n-select v-model:value="formModel.roleId" :options="options" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="是否启用" path="enableLogin">
          <n-switch v-model:value="formModel.enableLogin" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="余额">
          <n-input-number v-model:value="formModel.balance" />
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
import type { FormInst, FormRules } from 'naive-ui';
import { genderOptions } from '@/constants';
import { fetchUpdateUser, fetchAddUser, fetchGetRoleSelect } from '@/service';
import { formRules, createRequiredFormRule } from '@/utils';
// import { Model } from 'echarts';
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
  editData?: UserManagement.User | null;
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
      getRoleData();
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
    add: '添加用户',
    edit: '编辑用户'
  };
  return titles[props.type];
});

const formRef = ref<HTMLElement & FormInst>();

type FormModel = Pick<
  UserManagement.User,
  'userId' | 'userName' | 'gender' | 'userPhoneNum' | 'userEmail' | 'enableLogin' | 'roleId' | 'balance'
>;

const formModel = reactive<FormModel>(createDefaultFormModel());

const rules: FormRules = {
  userName: createRequiredFormRule('请输入用户名'),
  gender: createRequiredFormRule('请选择性别'),
  userPhoneNum: formRules.phone,
  userEmail: formRules.email
};

function createDefaultFormModel(): FormModel {
  return {
    userId: 0,
    userName: '',
    gender: null,
    userPhoneNum: '',
    userEmail: null,
    enableLogin: true,
    roleId: 10,
    balance: 0
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
      const { data } = await fetchAddUser(
        formModel.userName,
        formModel.userEmail,
        formModel.userPhoneNum,
        formModel.gender,
        formModel.enableLogin,
        formModel.roleId
      );
      if (data) {
        window.$message?.success('新增成功!');
        closeModal();
        emit('updateDataTable');
      }
    },
    edit: async () => {
      if (props.editData) {
        const { data } = await fetchUpdateUser(
          formModel.userId,
          formModel.userName,
          null,
          formModel.userEmail,
          formModel.userPhoneNum,
          formModel.gender,
          formModel.enableLogin,
          formModel.roleId,
          formModel.balance
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
async function getRoleData() {
  const { data } = await fetchGetRoleSelect();
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
getRoleData();
</script>

<style scoped></style>
