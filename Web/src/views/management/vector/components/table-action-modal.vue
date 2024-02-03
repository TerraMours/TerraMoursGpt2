<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-form ref="formRef" label-placement="left" :label-width="80" :model="formModel">
      <n-grid :cols="24" :x-gap="18">
        <n-form-item-grid-item v-if="props.type != 'edit'" :span="24" label="向量化" path="roleName">
          <n-switch v-model:value="isEmbedding">
            <template #checked>向量化</template>
            <template #unchecked>普通</template>
          </n-switch>
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="24" label="NameSpase">
          <n-input v-model:value="formModel.namespace" :disabled="props.type == 'edit'" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="24" label="ID">
          <n-input v-model:value="formModel.id" :disabled="props.type == 'edit'" />
        </n-form-item-grid-item>
        <n-form-item-grid-item v-if="isEmbedding == false" :span="24" label="values">
          <n-input v-model:value="valuesAsString" :disabled="props.type == 'edit'" />
        </n-form-item-grid-item>
        <n-form-item-grid-item :span="24" label="metadata">
          <n-input v-model:value="metadateValue" type="textarea" placeholder="metadata" default-value="{}" />
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
import { fetchEmbaddingUpsert, fetchUpdateVector, fetchUpsertVector } from '@/service';

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
  editData?: KnowledgeManagement.ScoredVector | null;
}

export type ModalType = NonNullable<Props['type']>;

defineOptions({ name: 'TableActionModal' });

const props = withDefaults(defineProps<Props>(), {
  type: 'add',
  editData: null
});

interface Emits {
  (e: 'update:visible', visible: boolean): void;
  (e: 'updateDataTable'): void; // 添加一个名为'closeModal'的自定义事件
}

const emit = defineEmits<Emits>();
const knowledgeId = 3;
const isEmbedding = ref(false);
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
    add: '添加敏感词',
    edit: '编辑敏感词'
  };
  return titles[props.type];
});

const formRef = ref<HTMLElement & FormInst>();

type FormModel = Pick<
  KnowledgeManagement.ScoredVector,
  'namespace' | 'knowledgeId' | 'id' | 'score' | 'values' | 'metadata'
>;

const formModel = reactive<FormModel>(createDefaultFormModel());

const valuesAsString = ref('');

function floatArrayToString(arr: any) {
  let str = '';
  if (arr[0].children === undefined) {
    return str;
  }
  // let i = 0; i < arr.length; i ++ 新版本的JavaScript/TypeScript中，不再推荐使用 ++ 或 -- 作为一元操作符。相反，推荐使用 += 或 -= 1 作为替代。
  for (let i = 0; i < arr.length; i += 1) {
    if (i !== 0) {
      str += ', ';
    }
    str += arr[i].children;
  }
  return str;
}

const metadateValue = ref('{}');

function createDefaultFormModel(): FormModel {
  return {
    namespace: '',
    knowledgeId: 3,
    id: '',
    score: 0,
    values: [],
    metadata: null
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
        valuesAsString.value = floatArrayToString(props.editData.values);
        metadateValue.value = JSON.stringify(props.editData.metadata);
      }
    }
  };

  handlers[props.type]();
}

async function handleSubmit() {
  await formRef.value?.validate();
  formModel.values = valuesAsString.value.split(',').map(parseFloat);
  formModel.metadata = JSON.parse(metadateValue.value);
  const handlers: Record<ModalType, () => void> = {
    add: async () => {
      if (!isEmbedding.value) {
        const { data } = await fetchUpsertVector(knowledgeId, [formModel], formModel.namespace);
        if (data) {
          window.$message?.success('新增成功!');
          closeModal();
          emit('updateDataTable');
        }
      } else {
        formModel.values = null;
        const { data } = await fetchEmbaddingUpsert(knowledgeId, [formModel], formModel.namespace);
        if (data) {
          window.$message?.success('新增成功!');
          closeModal();
          emit('updateDataTable');
        }
      }
    },
    edit: async () => {
      if (props.editData) {
        const { data } = await fetchUpdateVector(
          knowledgeId,
          formModel.id,
          formModel.values,
          formModel.metadata,
          formModel.namespace
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
