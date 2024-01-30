<template>
  <div class="h-full overflow-y-auto">
    <n-card  :bordered="false" class="rounded-16px shadow-sm">
      <n-space class="pb-12px" justify="space-between">
        <n-space>
          <n-button type="primary" @click="handleAddTable">
            <icon-ic-round-plus class="mr-4px text-20px" />
            新增
          </n-button>
          <n-button type="error">
            <icon-ic-round-delete class="mr-4px text-20px" />
            删除
          </n-button>
        </n-space>
        <n-space align="center" :size="18">
          <n-button size="small" type="primary" @click="getTableData">
            <icon-mdi-refresh class="mr-4px text-16px" :class="{ 'animate-spin': loading }" />
            刷新表格
          </n-button>
          <column-setting v-model:columns="columns" />
        </n-space>
      </n-space>
			<n-space class="pb-12px" justify='end'>
				<n-input-group>
					<n-input v-model:value="queryString" placeholder="请输入 名称" size="large" clearable/>
				</n-input-group>
			</n-space>
      <n-data-table :columns="columns" :data="tableData" :loading="loading" :pagination="pagination" />
      <table-action-modal v-model:visible="visible" :type="modalType" :edit-data="editData" @updateDataTable="getTableData"/>
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import { reactive, ref } from 'vue';
import type { Ref } from 'vue';
import { NButton, NPopconfirm, NSpace, NTag } from 'naive-ui';
import type { DataTableColumns, PaginationProps } from 'naive-ui';
import {fetchDeleteKnowledge, fetchKnowledgeList} from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';
import ColumnSetting from './components/column-setting.vue';
import {routeName} from "@/router";
import {useRouterPush} from "@/composables";

const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();
const queryString = ref<string>('')
const { routerPush } = useRouterPush();
const tableData = ref<KnowledgeManagement.Knowledge[]>([]);
function setTableData(data: KnowledgeManagement.Knowledge[]) {
  tableData.value = data;
}
function toVectorModule(knowledgeId:number) {
	routerPush({ name: routeName('management_vector'), query: { knowledgeId:knowledgeId }});
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

const columns: Ref<DataTableColumns<KnowledgeManagement.Knowledge>> = ref([
  {
    type: 'selection',
    align: 'center',
  },
  {
    key: 'index',
    title: '序号',
    align: 'center',
  },
  {
    key: 'knowledgeName',
    title: '名称',
    align: 'center'
  },
  {
    key: 'knowledgeType',
    title: '类型',
    align: 'center',
    render: row => {
      if (row.knowledgeType == 7) {
        return <NTag type="success">pgvector</NTag>;
      }
      else if (row.knowledgeType == 6) {
        return <NTag type="success">pinecone</NTag>;
      }
      return <NTag type="error">未知</NTag>;
    }
  },
  {
    key: 'apiKey',
    title: 'Key',
    align: 'center'
  },
  {
    key: 'workSpace',
    title: '工作空间',
    align: 'center'
  },
  {
    key: 'indexName',
    title: 'index',
    align: 'center'
  },
  {
    key: 'nameSpace',
    title: '命名空间',
    align: 'center'
  },
  {
    key: 'baseUrl',
    title: '代理地址',
    align: 'center',
  },
  {
    key: 'isCommon',
    title: '状态',
    align: 'center',
    render: row => {
      if (row.isCommon) {
        return <NTag type="success">启用</NTag>;
      }
      return <NTag type="error">停用</NTag>;
    }
  },
  {
    key: 'actions',
    title: '操作',
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
          <NButton size={'small'} onClick={() => toVectorModule(row.knowledgeId)}>
            设置
          </NButton>
          <NButton size={'small'} onClick={() => handleEditTable(row.knowledgeId)}>
            编辑
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDeleteTable(row.knowledgeId)}>
            {{
              default: () => '确认删除',
              trigger: () => <NButton size={'small'}>删除</NButton>
            }}
          </NPopconfirm>
        </NSpace>
      );
    },
  }
]) as Ref<DataTableColumns<KnowledgeManagement.Knowledge>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<KnowledgeManagement.Knowledge | null>(null);

function setEditData(data: KnowledgeManagement.Knowledge | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.knowledgeId === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await fetchDeleteKnowledge(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.knowledgeId === rowId);
    if (idx > -1) {
      tableData.value.splice(idx, 1);
    }
  }
}

const pagination: PaginationProps = reactive({
  page: 1,
  pageSize: 10,
  showSizePicker: true,
  pageSizes: [10, 15, 20, 25, 30],
  onChange: (page: number) => {
    pagination.page = page;
  },
  onUpdatePageSize: (pageSize: number) => {
    pagination.pageSize = pageSize;
    pagination.page = 1;
  }
});
function init() {
  getTableData();
}

// 初始化
init();
</script>

<style scoped></style>
