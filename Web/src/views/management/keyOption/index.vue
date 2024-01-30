<template>
  <div class="h-full overflow-hidden">
    <n-card title="Key池管理" :bordered="false" class="rounded-16px shadow-sm">
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
          <!-- <n-button type="success">
            <icon-uil:export class="mr-4px text-20px" />
            导出Excel
          </n-button> -->
        </n-space>
        <n-space align="center" :size="18">
          <n-button size="small" type="primary" @click="getTableData">
            <icon-mdi-refresh class="mr-4px text-16px" :class="{ 'animate-spin': loading }" />
            刷新表格
          </n-button>
        </n-space>
      </n-space>
      <n-data-table :columns="columns" :data="tableData" :loading="loading" :pagination="pagination" />
      <table-action-modal v-model:visible="visible" :type="modalType" :edit-data="editData" @updateDataTable="getTableData"/>
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import { reactive, ref } from 'vue';
import type { Ref } from 'vue';
import { NButton, NPopconfirm, NSpace } from 'naive-ui';
import type { DataTableColumns, PaginationProps } from 'naive-ui';
import { fetchKeyOptionList, fetchDeleteKeyOptions } from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';
const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();

const tableData = ref<GptManagement.KeyOption[]>([]);
const queryString = ref('');
function setTableData(data: GptManagement.KeyOption[]) {
  tableData.value = data;
}
const pagination: PaginationProps = reactive({
  page: 1,
  pageSize: 10,
  showSizePicker: true,
  pageSizes: [10, 15, 20, 25, 30],
  onChange: (page: number) => {
    pagination.page = page;
    getTableData();
  },
  onUpdatePageSize: (pageSize: number) => {
    pagination.pageSize = pageSize;
    pagination.page = 1;
    getTableData();
  }
});
function getQueryString() {
  return queryString.value;
}
async function getTableData() {
  startLoading();
  const { data } = await fetchKeyOptionList(getQueryString(), pagination.page, pagination.pageSize);
  if (data) {
    setTimeout(() => {
      setTableData(data.items);
      pagination.itemCount = data.total;
      pagination.pageCount = data.page;
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<GptManagement.KeyOption>> = ref([
  {
    type: 'selection',
    align: 'center'
  },
  {
    key: 'index',
    title: '序号',
    align: 'center'
  },
  {
    key: 'apiKey',
    title: 'Key',
    align: 'center'
  },
  {
    key: 'expirationTime',
    title: '过期时间',
    align: 'center'
  },
  {
    key: 'used',
    title: '使用量',
    align: 'center'
  },
  {
    key: 'unUsed',
    title: '余额',
    align: 'center'
  },
  {
    key: 'total',
    title: '总量',
    align: 'center'
  },
  {
    key: 'createDate',
    title: '创建时间',
    align: 'center'
  },
  {
    key: 'actions',
    title: '操作',
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
          <NButton size={'small'} onClick={() => handleEditTable(row.keyId)}>
            编辑
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDeleteTable(row.keyId)}>
            {{
              default: () => '确认删除',
              trigger: () => <NButton size={'small'}>删除</NButton>
            }}
          </NPopconfirm>
        </NSpace>
      );
    }
  }
]) as Ref<DataTableColumns<GptManagement.KeyOption>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<GptManagement.KeyOption | null>(null);

function setEditData(data: GptManagement.KeyOption | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.keyId === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await fetchDeleteKeyOptions(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.keyId === rowId);
    if (idx > -1) {
      tableData.value.splice(idx, 1);
    }
  }
}

function init() {
  getTableData();
}

// 初始化
init();
</script>

<style scoped></style>
