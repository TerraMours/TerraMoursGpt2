<template>
  <div class="h-full overflow-y-auto">
    <n-card title="图片记录管理" :bordered="false" class="rounded-16px shadow-sm ">
      <n-space class="pb-12px" justify="space-between">
        <n-space align="center" :size="18">
          <n-button size="small" type="primary" @click="getTableData">
            <icon-mdi-refresh class="mr-4px text-16px" :class="{ 'animate-spin': loading }" />
            刷新表格
          </n-button>
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
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import { reactive, ref } from 'vue';
import type { Ref } from 'vue';
import {DataTableColumns, NButton, NSpace, PaginationProps} from 'naive-ui';
import { fetchAllImageList, ShareImage } from '@/service';
import { useLoading } from '@/hooks';

const { loading, startLoading, endLoading } = useLoading(false);
const queryString = ref(null);
const handleEnter = (event: KeyboardEvent) => {
	if (event.key === 'Enter') {
    searchData()
	}
}
const tableData = ref<GptManagement.Image[]>([]);
function setTableData(data: GptManagement.Image[]) {
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
async function searchData(){
  pagination.page = 1;
  getTableData();
}
const PublicChange = async (imageRecordId: number, isPublic: boolean) => {
		const { data } = await ShareImage(imageRecordId, isPublic)
		if (data){
			window.$message?.success('操作成功!')
		}
}
async function getTableData() {
  startLoading();
  const { data } = await fetchAllImageList(getQueryString(), pagination.page, pagination.pageSize);
  if (data) {
    setTimeout(() => {
      setTableData(data.items);
      pagination.itemCount = data.total;
      pagination.pageCount = data.page  =Math.ceil(data.total/data.pageSize);
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<GptManagement.Image>> = ref([
  {
      type: 'expand',
			title: '展开',
      renderExpand: (rowData) => {
				return <n-image src={rowData.imagUrl}></n-image>;
      }
    },
  {
    key: 'index',
    title: '序号',
    align: 'center',
  },
	{
		key: 'userName',
		title: '用户',
		align: 'center',
	},
  {
    key: 'modelType',
    title: '模型类型',
    align: 'center',
    render: row => {
      switch (row.modelType) {
        case 0:
          return <n-tag type="success">CHATGPT</n-tag>;
        default:
          return <n-tag type="success">SD</n-tag>;
      }
    }
  },
  {
    key: 'model',
    title: '模型',
    align: 'center',
  },
  {
    key: 'prompt',
    title: '提示词',
    align: 'center',
    resizable: true,
    ellipsis: {
      tooltip: true
    }
  },
	{
		key: 'pranslatePrompt',
		title: '翻译文本',
		align: 'center',
		resizable: true,
		ellipsis: {
			tooltip: true
		}
	},
  {
    key: 'createDate',
    title: '创建时间',
    align: 'center',
		render: (rowData) => {
			const date = new Date(rowData.createDate);
			const formattedDate = `${date.getFullYear()}-${("0" + (date.getMonth() + 1)).slice(-2)}-${("0" + date.getDate()).slice(-2)}`;
			const formattedTime = `${("0" + date.getHours()).slice(-2)}:${("0" + date.getMinutes()).slice(-2)}:${("0" + date.getSeconds()).slice(-2)}`;
			return `${formattedDate} ${formattedTime}`;
		}

  },
	{
		key: 'isPublic',
		title: '是否公开',
		align: 'center',
		render: row => {
			return <n-switch v-model:value={row.isPublic} on-update:value={(value: boolean) =>{row.isPublic=value;
				PublicChange(row.imageRecordId,value)}}></n-switch>;
		}
	},
]) as Ref<DataTableColumns<GptManagement.Image>>;

function getQueryString() {
  return queryString.value;
}

function init() {
  getTableData();
}

// 初始化
init();
</script>

<style scoped></style>
