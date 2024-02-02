<template>
  <n-modal v-model:show="modalVisible" preset="card" :title="title" class="w-700px">
    <n-card :bordered="false" class="rounded-16px shadow-sm">
      <n-tabs type="segment">
        <n-tab-pane v-for="(tab, index) in getTabList" :key="index" :name="tab" :tab="tab">
          <template v-for="knowledge in getByType(tab)">
            <n-card :title="knowledge.knowledgeName" shadow="hover" hoverable style="max-width: 300px">
              <template #cover>
                <n-image
                  width="200"
                  height="200"
                  :src="knowledge.imagePath as string"
                  :intersection-observer-options="{ root: '#image-scroll-container' }"
                />
              </template>
              {{ knowledge.remark }}
              <div style="display: flex; justify-content: flex-end; margin-top: 24px">
                <n-button
                  strong
                  round
                  size="large"
                  type="primary"
                  @click="addChatConversation(knowledge.knowledgeId, knowledge.knowledgeName)"
                >
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
    </n-card>
  </n-modal>
</template>
<script setup lang="ts">
import { computed, ref } from 'vue';
import { fetchAddChatConversation, fetchKnowledgeList } from '@/service';
import { useChatState } from '@/store';
import { useLoading } from '@/hooks';
const chatStore = useChatState();
const getTabList = ['知识库', '普通'];
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
    add: '选择知识库',
    edit: '编辑知识库'
  };
  return titles[props.type];
});
const tableData = ref<KnowledgeManagement.Knowledge[]>([]);
function setTableData(data: KnowledgeManagement.Knowledge[]) {
  tableData.value = data;
}
function getByType(type: string) {
  if (type === '知识库') {
    return tableData.value;
  }
  return [
    {
      knowledgeId: 0,
      knowledgeName: '普通聊天',
      remark: '普通聊天',
      imagePath: 'https://07akioni.oss-cn-beijing.aliyuncs.com/07akioni.jpeg'
    }
  ];
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
async function addChatConversation(knowledgeId: number, knowledgeName: string) {
  const { data } = await fetchAddChatConversation(knowledgeName, knowledgeId);
  if (data) {
    window.$message?.success('新增成功!');
    chatStore.setActive(data.conversationId);
    closeModal();
    emit('updateDataTable');
  }
}
getTableData();
</script>
