<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-card  :bordered="false" class="rounded-16px shadow-sm ">
      <n-tabs type="segment">
        <n-tab-pane v-for="(tab, index) in getTabList" :key="index" :name="tab" :tab="tab">
          <template v-for="(knowledge) in tableData">
              <n-card :title="knowledge.knowledgeName" shadow="hover" hoverable style="max-width: 300px;">
                <template #cover>
                  <n-image  width="200" height="200" :src="knowledge.imagePath as string" :intersection-observer-options="{ root: '#image-scroll-container' }" />
                </template>
                {{knowledge.remark}}
                <div style="display: flex; justify-content: flex-end; margin-top: 24px;">
                  <n-button strong round size="large" type="primary" @click="addChatConversation(knowledge)">
                    <template #icon>
                      <SvgIcon class="text-xl" icon="ri:link" />
                    </template>
                    立即使用
                  </n-button>
                </div>
              </n-card>
          </template>
        </n-tab-pane>
      </n-tabs>
      <n-space justify="end">
        <n-button class="w-72px" @click="closeModal">取消</n-button>
      </n-space>
    </n-card>
  </n-modal>
</template>
<script setup lang="ts">
import {computed, ref} from "vue/dist/vue";
import {fetchAddChatConversation, fetchKnowledgeList} from "@/service";
import {useLoading} from "@/hooks";

const getTabList= ['全部'];
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
const { startLoading, endLoading } = useLoading(false);
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
const tableData = ref<KnowledgeManagement.Knowledge[]>([]);
function setTableData(data: KnowledgeManagement.Knowledge[]) {
  tableData.value = data;
}
async function getTableData() {
  startLoading();
  const { data } = await fetchKnowledgeList();
  if (data) {
    setTimeout(() => {
      setTableData(data);
      endLoading();
    }, 1000);
  }
}
async function addChatConversation(knowledge: ApiKnowledgeManagement.Knowledge) {
  const { data } = await fetchAddChatConversation(knowledge.knowledgeName,knowledge.knowledgeId);
  if (data) {
    window.$message?.success('新增成功!');
    closeModal();
    emit('updateDataTable');
  }
}
getTableData();
</script>
