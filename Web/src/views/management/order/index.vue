<template>
  <div class="h-full overflow-y-auto">
    <n-card title="订单列表" :bordered="false" class="rounded-16px shadow-sm ">
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
import {fetchOrderList} from '@/service';
import { useLoading } from '@/hooks';

const { loading, startLoading, endLoading } = useLoading(false);
const queryString = ref(null);
const handleEnter = (event: KeyboardEvent) => {
	if (event.key === 'Enter') {
    searchData()
	}
}
const tableData = ref<PayManagement.Order[]>([]);
function setTableData(data: PayManagement.Order[]) {
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
async function getTableData() {
  startLoading();
  const { data } = await fetchOrderList(getQueryString(), pagination.page, pagination.pageSize);
  if (data) {
    setTimeout(() => {
      setTableData(data.items);
      pagination.itemCount = data.total;
      pagination.pageCount = data.page  =Math.ceil(data.total/data.pageSize);
      endLoading();
    }, 1000);
  }
}

const columns: Ref<DataTableColumns<PayManagement.Order>> = ref([
  {
    key: 'index',
    title: '序号',
    align: 'center',
  },
    {
        key: 'orderId',
        title: '订单号',
        align: 'center',
    },
    {
        key: 'name',
        title: '订单名称',
        align: 'center',
    },
    {
        key: 'price',
        title: '订单金额',
        align: 'center',
    },
    {
        key: 'isVIP',
        title: '订单类型',
        align: 'center',
        render: row => {
            if (row.isVIP) {
                return <n-tag type="success">会员</n-tag>;
            }
            return <n-tag type="info">充值</n-tag>;
        }
    },
    {
        key: 'vipLevel',
        title: '会员等级',
        align: 'center',
    },
    {
        key: 'vipTime',
        title: '充值时间（月）',
        align: 'center',
    },
  {
    key: 'userName',
    title: '发起用户',
    align: 'center',
  },
    {
        key: 'tradeNo',
        title: '交易号',
        align: 'center',
    },
  {
    key: 'status',
    title: '订单状态',
    align: 'center',
		render: row => {
        switch (row.status) {
            case 'TRADE_SUCCESS':
                return <n-tag type="success">支付成功</n-tag>;
            case 'TRADE_FINISHED' :
                return <n-tag type="success">交易结束</n-tag>;
            case 'WAIT_BUYER_PAY':
                return <n-tag type="warning">等待买家付款</n-tag>;
            case 'TRADE_CLOSED':
                return <n-tag type="error">交易超时关闭</n-tag>;
            default:
                return <n-tag type="error">交易情况未知</n-tag>;
        }
		}
  },
  {
    key: 'createdTime',
    title: '创建时间',
    align: 'center',
		render: (rowData) => {
			const date = new Date(rowData.createdTime);
			const formattedDate = `${date.getFullYear()}-${("0" + (date.getMonth() + 1)).slice(-2)}-${("0" + date.getDate()).slice(-2)}`;
			const formattedTime = `${("0" + date.getHours()).slice(-2)}:${("0" + date.getMinutes()).slice(-2)}:${("0" + date.getSeconds()).slice(-2)}`;
			return `${formattedDate} ${formattedTime}`;
		}
  },
    {
        key: 'paidTime',
        title: '支付时间',
        align: 'center',
        render: (rowData) => {
            if(rowData.paidTime ==null)
                return '';
            const date = new Date(rowData.paidTime);
            const formattedDate = `${date.getFullYear()}-${("0" + (date.getMonth() + 1)).slice(-2)}-${("0" + date.getDate()).slice(-2)}`;
            const formattedTime = `${("0" + date.getHours()).slice(-2)}:${("0" + date.getMinutes()).slice(-2)}:${("0" + date.getSeconds()).slice(-2)}`;
            return `${formattedDate} ${formattedTime}`;
        }
    }
]) as Ref<DataTableColumns<PayManagement.Order>>;

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
