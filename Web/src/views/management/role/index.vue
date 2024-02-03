<template>
  <div class="h-full overflow-y-auto">
    <n-card title="角色管理" :bordered="false" class="rounded-16px shadow-sm">
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
      <menu-setting v-model:edit-visible="menuVisible" :role-id="checkRoleId" />
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import { reactive, ref } from 'vue';
import type { Ref } from 'vue';
import { NButton, NPopconfirm, NSpace, NTag } from 'naive-ui';
import type { DataTableColumns, PaginationProps } from 'naive-ui';
import { fetchRoleList, fetchDelRole } from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';
import MenuSetting from './components/menu-setting.vue';
const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();
const { bool: menuVisible, setTrue: openMenu } = useBoolean();

const tableData = ref<UserManagement.Role[]>([]);
function setTableData(data: UserManagement.Role[]) {
  tableData.value = data;
}

async function getTableData() {
  startLoading();
  const { data } = await fetchRoleList();
  if (data) {
    setTimeout(() => {
      setTableData(data);
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<UserManagement.Role>> = ref([
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
    key: 'roleName',
    title: '角色名称',
    align: 'center'
  },
  {
    key: 'createDate',
    title: '创建时间',
    align: 'center'
  },
	{
		key: 'externalUrl',
		title: '角色特性',
		align: 'center',
		render: row => {
			if (row.isAdmin) {
				return <NTag type="success">系统管理员</NTag>;
			}
			else if(row.isNewUser) {
				return <NTag type="success">默认角色</NTag>;
			}
			return <NTag type="success">普通</NTag>;
		}
	},
  {
    key: 'modifyDate',
    title: '更新时间',
    align: 'center'
  },
  {
    key: 'actions',
    title: '操作',
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
          <NButton size={'small'} onClick={() => handleMenu(row.roleId)}>
            设置权限
          </NButton>
          <NButton size={'small'} onClick={() => handleEditTable(row.roleId)}>
            编辑
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDeleteTable(row.roleId)}>
            {{
              default: () => '确认删除',
              trigger: () => <NButton size={'small'}>删除</NButton>
            }}
          </NPopconfirm>
        </NSpace>
      );
    }
  }
]) as Ref<DataTableColumns<UserManagement.Role>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<UserManagement.Role | null>(null);
const checkRoleId = ref<number | null>(null);
function setCheckRoleId(data: number | null) {
  checkRoleId.value = data;
}

function setEditData(data: UserManagement.Role | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleMenu(rowId: number) {
  openMenu();
  setCheckRoleId(rowId);
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.roleId === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await fetchDelRole(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.roleId === rowId);
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
