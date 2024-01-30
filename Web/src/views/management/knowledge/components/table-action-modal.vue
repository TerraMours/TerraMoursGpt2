<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-form ref="formRef" label-placement="left" :label-width="80" :model="formModel" :rules="rules">
      <n-grid :cols="24" :x-gap="18">
        <n-form-item-grid-item :span="24" label="图片" path="roleName">
          <n-upload
            v-model:file-list="fileList"
            :action="uploadUrl"
            :headers="{ Authorization: `Bearer ${localStg.get('token')}` }"
            list-type="image-card"
            :max="1"
            accept="image/png, image/jpeg"
            @finish="handleFinish"
          />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="名称">
          <n-input v-model:value="formModel.knowledgeName" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="类型">
          <n-select v-model:value="formModel.knowledgeType" size="medium" :options="ModelOptions" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="24" label="apiKey">
          <n-input v-model:value="formModel.apiKey" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="工作空间">
          <n-input v-model:value="formModel.workSpace" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="index">
          <n-input v-model:value="formModel.indexName" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="命名空间">
          <n-select
            v-model:value="formModel.nameSpace"
            class="mr-10px"
            filterable
            tag
            placeholder="your NameSpase"
            clearable
            :options="nameSpaceOptions"
          />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="代理地址">
          <n-input v-model:value="formModel.baseUrl" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="12" label="是否启用">
          <n-switch v-model:value="formModel.isCommon" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="24" label="介绍说明">
          <n-input v-model:value="formModel.remark" type="textarea" />
        </n-form-item-grid-item>
      </n-grid>
      <n-space class="w-full pt-16px" :size="24" justify="space-between">
        <n-space justify="start">
          <n-button type="primary" @click="indexStats">连接测试</n-button>
          <h3 class="mr-10px text-20px">VECTOR COUNT:</h3>
          <h3 class="mr-10px text-20px">{{ vectorCount }}</h3>
        </n-space>
        <n-space justify="end">
          <n-button class="w-72px" @click="closeModal">取消</n-button>
          <n-button class="w-72px" type="primary" @click="handleSubmit">确定</n-button>
        </n-space>
      </n-space>
    </n-form>
  </n-modal>
</template>

<script setup lang="ts">
import { ref, computed, reactive, watch } from 'vue';
import type { FormInst, FormRules, UploadFileInfo } from 'naive-ui';
import { fetchAddKnowledge, fetchUpdateKnowledge, fetchKnowledgeDescribeIndex, apiUrl } from '@/service';
import { formRules, createRequiredFormRule, localStg } from '@/utils';
const ModelOptions: { label: string; value: number; disabled: boolean }[] = [
  { label: 'pgvector', value: 7, disabled: false },
  { label: 'pinecone', value: 6, disabled: false }
];
const fileList = ref<UploadFileInfo[]>();
const uploadUrl = `${apiUrl}/api/v1/Product/UploadProductImage`;
const vectorCount = ref(0);
const nameSpaceOptions: { label: string; value: string; disabled: boolean }[] = [];
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
  editData?: KnowledgeManagement.Knowledge | null;
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
    add: '添加知识库',
    edit: '编辑知识库'
  };
  return titles[props.type];
});

const formRef = ref<HTMLElement & FormInst>();

type FormModel = Pick<
  KnowledgeManagement.Knowledge,
  | 'knowledgeId'
  | 'isCommon'
  | 'knowledgeType'
  | 'apiKey'
  | 'indexName'
  | 'workSpace'
  | 'baseUrl'
  | 'knowledgeName'
  | 'nameSpace'
  | 'imagePath'
  | 'remark'
>;
const formModel = reactive<FormModel>(createDefaultFormModel());

const rules: FormRules = {
  userName: createRequiredFormRule('请输入用户名'),
  gender: createRequiredFormRule('请选择性别'),
  userPhoneNum: formRules.phone,
  userEmail: formRules.email
};
const handleFinish = ({ file, event }: { file: UploadFileInfo; event?: ProgressEvent }) => {
  const res = JSON.parse((event?.target as XMLHttpRequest).response);
  file.url = `${apiUrl}${res.data}`;
  return file;
};
function createDefaultFormModel(): FormModel {
  return {
    knowledgeId: 0,
    knowledgeName: '',
    isCommon: true,
    knowledgeType: 7,
    apiKey: '',
    indexName: '',
    nameSpace: '',
    workSpace: '',
    baseUrl: '',
    imagePath: null,
    remark: null
  };
}

function handleUpdateFormModel(model: Partial<FormModel>) {
  Object.assign(formModel, model);
  if (model.imagePath !== null) {
    fileList.value = [
      {
        id: 'c',
        name: '我是自带url的图片.png',
        status: 'finished',
        url: model.imagePath
      }
    ];
  } else {
    fileList.value = [];
  }
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
  if (fileList.value !== null && fileList.value![0] !== null) formModel.imagePath = fileList.value![0].url;
  const handlers: Record<ModalType, () => void> = {
    add: async () => {
      const { data } = await fetchAddKnowledge(
        formModel.knowledgeName,
        formModel.apiKey,
        formModel.indexName,
        formModel.nameSpace,
        formModel.workSpace,
        formModel.baseUrl,
        formModel.isCommon,
        formModel.knowledgeType,
        formModel.imagePath,
        formModel.remark
      );
      if (data) {
        window.$message?.success('新增成功!');
        closeModal();
        emit('updateDataTable');
      }
    },
    edit: async () => {
      if (props.editData) {
        const { data } = await fetchUpdateKnowledge(
          formModel.knowledgeId,
          formModel.knowledgeName,
          formModel.apiKey,
          formModel.indexName,
          formModel.nameSpace,
          formModel.workSpace,
          formModel.baseUrl,
          formModel.isCommon,
          formModel.knowledgeType,
          formModel.imagePath,
          formModel.remark
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

async function indexStats() {
  const { data } = await fetchKnowledgeDescribeIndex(
    formModel.knowledgeName,
    formModel.apiKey,
    formModel.indexName,
    formModel.nameSpace,
    formModel.workSpace,
    formModel.baseUrl,
    formModel.isCommon,
    formModel.knowledgeType
  );
  if (data) {
    window.$message?.success('测试连接成功!');
    vectorCount.value = data.totalVectorCount;

    for (const [key] of Object.entries(data.namespaces)) {
      const option = {
        label: key,
        value: key,
        disabled: false
      };

      nameSpaceOptions.push(option);
    }
    if (formModel.nameSpace === null || formModel.nameSpace === '') {
      formModel.nameSpace = nameSpaceOptions[0].value;
    }
  }
}
</script>

<style scoped></style>
