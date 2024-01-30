<template>
  <div class="h-full overflow-y-auto">
    <n-card title="菜单管理" :bordered="false" class="rounded-16px shadow-sm">
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
			<n-space class="pb-12px" justify='end'>
				<n-input-group>
					<n-input v-model:value="queryString" placeholder="请输入 名称/url" size="large" clearable/>
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
import { NButton, NPopconfirm, NSpace, NTag } from 'naive-ui';
import type { DataTableColumns, PaginationProps } from 'naive-ui';
import { fetchMenuList, fetchDelMenu } from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';

const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();
const queryString = ref<string>('')
const tableData = ref<UserManagement.Menu[]>([]);
function setTableData(data: UserManagement.Menu[]) {
  tableData.value = data;
}

async function getTableData() {
  startLoading();
  const { data } = await fetchMenuList();
  if (data) {
    setTimeout(() => {
      setTableData(data);
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<UserManagement.Menu>> = ref([
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
    key: 'menuName',
    title: '菜单名称',
    align: 'center'
  },
  {
    key: 'menuUrl',
    title: '菜单url地址',
    align: 'center'
  },
  {
    key: 'externalUrl',
    title: '是否外链',
    align: 'center',
    render: row => {
      if (row.externalUrl) {
        return <NTag type="success">是</NTag>;
      }
      return <NTag type="error">否</NTag>;
    }
  },
  {
    key: 'isShow',
    title: '是否可见',
    align: 'center',
    render: row => {
      if (row.isShow) {
        return <NTag type="success">是</NTag>;
      }
      return <NTag type="error">否</NTag>;
    }
  },
  {
    key: 'isHome',
    title: '是否首页',
    align: 'center',
    render: row => {
      if (row.isHome) {
        return <NTag type="success">是</NTag>;
      }
      return <NTag type="error">否</NTag>;
    }
  },
  {
    key: 'actions',
    title: '操作',
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
          <NButton size={'small'} onClick={() => handleEditTable(row.menuId)}>
            编辑
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDeleteTable(row.menuId)}>
            {{
              default: () => '确认删除',
              trigger: () => <NButton size={'small'}>删除</NButton>
            }}
          </NPopconfirm>
        </NSpace>
      );
    }
  }
]) as Ref<DataTableColumns<UserManagement.Menu>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<UserManagement.Menu | null>(null);

function setEditData(data: UserManagement.Menu | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.menuId === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await fetchDelMenu(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.menuId === rowId);
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

const dataSource= computed<UserManagement.Menu[]>(() => {
	const data = tableData.value;
	const value = queryString.value
	if (value && value !== '') {
		return data.filter((item: UserManagement.Menu) => {
			return item.menuName?.includes(value) || item.menuUrl?.includes(value)
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
