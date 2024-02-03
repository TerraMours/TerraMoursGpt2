<template>
  <div class="h-full overflow-y-auto">
    <n-card title="商品管理" :bordered="false" class="rounded-16px shadow-sm">
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
        </n-space>
      </n-space>
			<n-space class="pb-12px" justify='end'>
				<n-input-group>
					<n-input v-model:value="queryString" placeholder="请输入 名称/描述" size="large" clearable/>
				</n-input-group>
			</n-space>
      <n-data-table :columns="columns" :data="dataSource" :loading="loading" :pagination="pagination" />
      <table-action-modal v-model:visible="visible" :type="modalType" :edit-data="editData" @updateDataTable="getTableData"/>
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import { computed, reactive, ref } from 'vue';
import type { Ref } from 'vue';
import { NButton, NPopconfirm, NSpace } from 'naive-ui';
import type { DataTableColumns, PaginationProps } from 'naive-ui';
import { fetchAllProductList, DeleteProduct } from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';
const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();
const queryString = ref<string>('')
const tableData = ref<PayManagement.Product[]>([]);
function setTableData(data: PayManagement.Product[]) {
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
async function getTableData() {
  startLoading();
  const { data } = await fetchAllProductList();
  if (data) {
    setTimeout(() => {
      setTableData(data);
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<PayManagement.Product>> = ref([
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
    key: 'name',
    title: '商品名称',
    align: 'center'
  },
  {
    key: 'description',
    title: '商品描述',
    align: 'center'
  },
  {
    key: 'price',
    title: '商品价格',
    align: 'center'
  },
  {
    key: 'discount',
    title: '商品折扣',
    align: 'center'
  },
  {
    key: 'categoryId',
    title: '商品分类',
    align: 'center'
  },
  {
    key: 'isVIP',
    title: '是否VIP',
    align: 'center',
		render: row => {
			if (row.isVIP) {
				return <n-tag type="success">是</n-tag>;
			}
			return <n-tag type="error">否</n-tag>;
		}
  },
  {
    key: 'vipLevel',
    title: 'VIP等级',
    align: 'center'
  },
  {
    key: 'vipTime',
    title: '充值时间（月）',
    align: 'center'
  },
  {
    key: 'actions',
    title: '操作',
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
          <NButton size={'small'} onClick={() => handleEditTable(row.id)}>
            编辑
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDeleteTable(row.id)}>
            {{
              default: () => '确认删除',
              trigger: () => <NButton size={'small'}>删除</NButton>
            }}
          </NPopconfirm>
        </NSpace>
      );
    }
  }
]) as Ref<DataTableColumns<PayManagement.Product>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<PayManagement.Product | null>(null);

function setEditData(data: PayManagement.Product | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.id === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await DeleteProduct(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.id === rowId);
    if (idx > -1) {
      tableData.value.splice(idx, 1);
    }
  }
}
const dataSource= computed<PayManagement.Product[]>(() => {
	const data = tableData.value;
	const value = queryString.value
	if (value && value !== '') {
		return data.filter((item: PayManagement.Product) => {
			return item.name?.includes(value) || item.description?.includes(value)
		})
	}
	return data
})
function init() {
  getTableData();
}

// 初始化
init();
</script>

<style scoped></style>
