<template>
  <div class="h-full overflow-y-auto">
    <n-card title="系统提示词管理" :bordered="false" class="rounded-16px shadow-sm">
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
			<n-space class="pb-12px" justify="space-between">
      <n-space class="pb-12px" >
        <n-button
            :disabled="!fileList?.length"
            style="margin-bottom: 12px"
            @click="handleClick"

          >
            上传文件
          </n-button>
          <n-upload
            v-model:file-list="fileList"
            action="https://www.mocky.io/v2/5e4bafc63100007100d8b70f"
            :default-upload="false"
            @change="handleChange"
            :max="1"
          >
            <n-button>选择文件</n-button>
          </n-upload>
      </n-space>
				<n-space>
					<n-input-group>
            <n-input v-model:value="queryString" @keypress="handleEnter" placeholder="请输入..（支持Enter）" clearable/>
            <n-button type="primary" @click="searchData">
              搜索
            </n-button>
					</n-input-group>
				</n-space>
			</n-space>
      <n-data-table remote :columns="columns" :data="tableData" :loading="loading" :pagination="pagination" />
      <table-action-modal v-model:visible="visible" :type="modalType" :edit-data="editData" @updateDataTable="getTableData"/>
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import { reactive, ref } from 'vue';
import type { Ref } from 'vue';
import { NButton, NPopconfirm, NSpace } from 'naive-ui';
import type { DataTableColumns, PaginationProps, UploadFileInfo } from 'naive-ui';
import { fetchPromptOptionList, fetchDeletePromptOption,ImportPromptOptionByFile } from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';
const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();
const fileList = ref<UploadFileInfo[]>()

const tableData = ref<GptManagement.PromptOption[]>([]);
const queryString = ref(null);
function setTableData(data: GptManagement.PromptOption[]) {
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
  },
  prefix ({ itemCount }) {
        return `Total is ${itemCount}.`
      }
});
function getQueryString() {
  return queryString.value;
}
const handleEnter = (event: KeyboardEvent) => {
	if (event.key === 'Enter') {
    searchData()
	}
}
async function searchData(){
  pagination.page = 1;
  getTableData();
}
async function getTableData() {
  startLoading();
  const { data } = await fetchPromptOptionList(getQueryString(), pagination.page, pagination.pageSize);
  if (data) {
    setTimeout(() => {
      setTableData(data.items);
      pagination.itemCount = data.total;
      pagination.pageCount = Math.ceil(data.total/data.pageSize);
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<GptManagement.PromptOption>> = ref([
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
    key: 'act',
    title: '扮演角色',
    align: 'center'
  },
  {
    key: 'prompt',
    title: '系统提示词',
    align: 'center',
    resizable: true,
    minWidth: 800,
    maxWidth: 1000,
    ellipsis: {
      tooltip: true
    }
  },
  {
    key: 'actions',
    title: '操作',
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
          <NButton size={'small'} onClick={() => handleEditTable(row.promptId)}>
            编辑
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDeleteTable(row.promptId)}>
            {{
              default: () => '确认删除',
              trigger: () => <NButton size={'small'}>删除</NButton>
            }}
          </NPopconfirm>
        </NSpace>
      );
    }
  }
]) as Ref<DataTableColumns<GptManagement.PromptOption>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<GptManagement.PromptOption | null>(null);

function setEditData(data: GptManagement.PromptOption | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.promptId === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await fetchDeletePromptOption(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.promptId === rowId);
    if (idx > -1) {
      tableData.value.splice(idx, 1);
    }
  }
}

async function handleChange (options: { fileList: UploadFileInfo[] }) {
   fileList.value = options.fileList
      }
async function handleClick () {
        const {data}=await ImportPromptOptionByFile(fileList.value![0].file as File);
        if(data){
          window.$message?.success('导入成功!');
          fileList.value?.splice(0,1);
        }
      }

function init() {
  getTableData();
}

// 初始化
init();
</script>

<style scoped></style>
