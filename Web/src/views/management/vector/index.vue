<template>
  <div class="h-full overflow-y-auto">
    <n-card  :bordered="false" class="rounded-16px shadow-sm">
      <n-space class="pb-12px" justify="space-between">
        <n-space>
          <n-button type="primary" @click="toKnowledgeModule">
            <icon-ic-round-plus class="mr-4px text-20px" />
            返回
          </n-button>

        </n-space>
        <n-space align="center" :size="18">
          <n-button size="small" type="primary" >
            <icon-mdi-refresh class="mr-4px text-16px"  />
            Connect
          </n-button>
        </n-space>
      </n-space>
			<n-space vertical class="pb-20px ">
					<gradient-bg  start-color="#ec4786" end-color="#b955a4">
								<h3 class="text-50px">{{knowledge?.knowledgeName}}</h3>
								<div class="flex justify-between pt-12px">
									<n-space>
									<n-space vertical align="start">
										<h3 class="mr-10px text-20px">METRIC</h3>
										<h3 class="mr-10px text-15px" >cosin</h3>
									</n-space>
									<n-space vertical align="start">
										<h3 class="mr-10px text-20px">Host</h3>
										<h3 class="mr-10px text-15px" >{{knowledge?.baseUrl}}</h3>
									</n-space>
									</n-space>
									<n-space vertical align="end">
										<h3 class="mr-10px text-20px">VECTOR COUNT</h3>
										<h3 class="mr-10px text-15px" >{{vectorCount}}</h3>
									</n-space>
								</div>
					</gradient-bg>
			</n-space>
      <n-space class="pb-12px" vertical>
        <n-switch class="mr-10px" v-model:value="isEmbedding" size="small" @update-value="setQuery">
          <template #checked>向量</template>
          <template #unchecked>普通</template>
        </n-switch>
          <n-input-group >
        <n-select class="mr-10px" filterable tag  placeholder="your NameSpase" clearable v-model:value="nameSpaceString"  :style="{ width: '20%' }" :options="nameSpaceOptions">
        </n-select>
        <n-input-group class="mr-10px" :style="{ width: '70%' } " v-if="!isEmbedding">
          <n-select :style="{ width: '20%' }" :options="Options" v-model:value="selectType" @update-value="setQuery"/>
          <n-input :style="{ width: '80%' }"  clearable v-model:value="queryString" />
        </n-input-group>
        <n-input :style="{ width: '70%' }"  clearable v-model:value="queryString" v-else/>
        <n-input-number class="mr-10px"  v-model:value="topk"  :style="{ width: '8%' }">
          <template #prefix>
            Top K:
          </template>
        </n-input-number>
          <n-button type="primary"  @click="getTableData">
            搜索
          </n-button>
					</n-input-group>
      </n-space>
      <n-space class="pb-12px" vertical align="end">
        <n-button type="primary"  @click="handleAddTable">
          <icon-ic-round-plus class="mr-4px text-20px" />
          新增
        </n-button>
      </n-space>
      <n-data-table  :columns="columns" :data="tableData" :loading="loading" :pagination="pagination"/>
      <table-action-modal v-model:visible="visible" :type="modalType" :edit-data="editData" @updateDataTable="getTableData"/>
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import {DataTableColumns, NButton, NSpace, NPopconfirm, PaginationProps} from "naive-ui";
import {GradientBg} from "@/views/dashboard/analysis/components/data-card/components";
import {reactive, Ref, ref} from "vue";
import {
  fetchDelUser,
  fetchVectorList,
  fetchEmbaddingVectorList,
  fetchQueryKnowledge,
  fetchDescribeIndexStats
} from "@/service";
import {useBoolean, useLoading} from "@/hooks";
import {ModalType} from "@/views/management/role/components/table-action-modal.vue";
import TableActionModal from './components/table-action-modal.vue';
import {useRoute} from "vue-router";
import {routeName} from "@/router";
import {useRouterPush} from "@/composables";
const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();
const Options: { label: string; value: number,disabled: boolean}[] = [
	{ label: 'Query by Vector', value: 1,disabled:false },
	{ label: 'Query by ID', value: 2,disabled:false },
]
const route = useRoute();
const { routerPush } = useRouterPush();
function toKnowledgeModule() {
    routerPush({ name: routeName('management_knowledge')});
}
let nameSpaceOptions: { label: string; value: string,disabled: boolean}[] = []
const nameSpaceString=ref<string>();
const vector = '0.48,0.63,0.62,0.81,0.37,0.7,0.14,0.48,0.62,0.72,0.92,0.13,0.23,0.15,0.27,0.85,0.08,0.67,0.44,0.88,0.06,0.43,0.29,0.02,0.99,0.39,0,0.32,0.1,0.21,0.22,0.49,0.59,0.97,0.36,0.26,0.36,0.32,0.56,0.68,0.87,0.31,0.14,0.27,0.79,0.19,0.99,0.47,0.24,0.38,0.11,0.8,0.05,0.09,0.57,0.43,0.79,0.54,0.68,0.71,0.2,0.95,0.28,0.97,0.9,0.74,0.26,0.58,0.64,0.01,0.07,0.1,0.69,0.14,0.46,0.87,0.82,0.79,0.27,0.63,0.77,0.27,0.45,0.62,0.7,0.12,0.26,0.43,0.61,0.75,0.93,0.08,0.93,0.66,0.84,0.06,0.98,0.03,0.41,0.25,0.68,0.45,0.78,0.77,0.07,0.37,0.11,0.23,0.5,0.41,0.44,0.76,0.16,0.72,0.39,0.11,0.29,0.08,0.85,0.71,0.93,0.02,0.59,0.14,0.82,0.14,0.68,0.25,0.13,0.14,0.25,0.13,0.32,0.61,0.69,0.61,0.15,0.77,0.61,0.01,0.95,0.81,0.29,0.84,0.7,0.37,0.76,0.85,0.49,0.05,0.26,0.6,0.73,0.82,0.9,0.76,0.1,0.01,0.74,0.66,0.89,0.66,0.21,0.66,0.02,0.52,0.04,0.69,0.23,0.34,0.99,0.3,0.24,0.46,0.82,0.85,0.44,0.15,0.39,0.45,0.51,0.58,0.39,0.71,0.74,0.86,0.58,0.84,0.06,0.33,0.43,0.64,0.62,0.39,0.89,0.46,0.28,0.65,0.53,0.31,0.93,0.68,0,0.8,0.65,0.52,0.53,0.97,0.86,0.02,0.1,0.67,0.26,0.66,0.54,0.51,0.82,0.73,0.31,0.23,0.16,0.99,0.11,0.99,0.47,0.74,0.53,0.96,0.8,0.91,0.42,0.6,0.25,0.03,0.16,0.39,0.19,0.29,0.57,0.66,0.36,0.99,0.91,0.44,0.19,0.37,0.21,0.75,0.39,0.38,0.44,0.22,0.78,0.85,0.69,0.24,0.31,0.25,0.9,0.48,0.06,0.89,0.35,0.04,0.39,0.41,0.24,0.93,0.5,0.54,0.11,0.56,0.92,0.51,0.27,0.4,0.73,0.44,0.54,0.64,0.99,0.45,0.61,0.96,0.61,0.51,0.99,0.63,0.87,0.28,0.41,0.24,0.35,0.97,0.07,0.96,0.84,0.11,0.68,0.85,0.92,0.36,0.17,0.41,0.19,0.21,0.32,0.09,0.39,0.92,0.59,0.1,0.1,0.76,0.4,0.81,0.82,0.92,0.69,0.67,0.9,0.67,0.76,0.26,0.84,0.73,0.08,0.72,0.07,0.79,0.11,0.78,0.66,0.05,0.74,0.64,0.16,1,0.35,0.66,0.07,0.32,0.61,0.65,0.35,0.99,0.24,0.96,0.28,0.94,0.1,0.05,0.63,0.31,0.72,0.4,0.78,0.86,0.41,0.34,0.33,0.64,0,0.6,0.03,0.66,0.06,0.93,0.99,0.04,0.2,0.35,0.53,0.13,0.14,0.87,0.54,1,0.13,0.15,0.89,0.16,0.79,0.4,0.38,0.64,0.7,0.38,0.46,0.5,0.91,0.88,0.48,0.77,0.17,0.63,0.96,0.51,0.74,0.98,0.86,0.55,0.22,0.73,0.86,0.58,0.29,0.6,0.23,0.84,0.42,0.84,0.81,0.99,0.87,0.99,0.15,0.25,0.37,0.2,0.79,0.9,0.14,0.75,0.61,0.14,0.63,0.89,0.62,0.01,0.98,0.03,0.86,0.92,0.51,0.31,0.49,0.89,0.99,0.97,0.35,0.39,0.39,0.35,0.92,0.13,0.45,0.12,0.99,0.44,0.43,0.49,0.93,0.4,0.11,0.13,0.8,0.32,0.37,0.37,0.7,0.99,0.84,0.09,0.83,0.84,0.09,0.8,0.95,0.08,0.61,0.11,0.4,0.91,0.87,0.95,0.83,0.69,0.75,0.33,0.5,0.77,0.37,0.9,0.23,0.58,0.86,0.25,0.27,0.82,0.13,0.16,0.26,0.42,0.12,0.74,0.9,0.85,0.06,0.09,0.79,0.26,0.02,0.28,0.92,0.3,0.47,0.45,0.9,0.16,0.53,0.19,0.62,0.9,0.94,0.08,0.2,0.03,0.67,0.26,0.38,0.09,0.24,0.9,0.2,0.48,0.53,0.16,0.67,0.54,0.86,0.57,0.88,0.45,0.11,0.55,0.31,0.49,0.7,0.15,0.14,0.08,0.95,0.83,0.85,0.75,0.36,0.61,0.79,0.65,0.84,0.22,0.65,0.09,0.89,0.83,0.57,0.66,0.04,0.18,0.19,0.21,0.13,0.95,0.98,0.57,0.23,0.84,0.97,0.6,0.4,0.39,0.92,0.9,0.74,0.26,0.24,0.97,0.64,0.37,0.34,0.38,0.64,0.69,0.1,0.43,0.79,0.86,0.22,0.18,0.92,0.8,0.84,0.77,0.79,0.22,0.6,0.65,0.09,0.84,0.71,0.64,0.24,0.7,0.17,0.41,0.99,0.53,0.6,0.97,0.26,0.02,0.62,0.87,0.76,0.31,0.42,0.59,0.25,0.66,0.19,0.88,0.18,0.83,0.09,0.14,0.52,0.95,0.54,0,0.38,0.96,0.23,0.05,0.83,0.32,0.02,0.81,0.06,0.79,0.63,0.03,0.35,0.39,0.86,0.71,0.23,0.33,0.22,0.23,0.82,0.14,0.71,0.88,0.48,0.2,0.21,0.38,0.93,0.61,0.84,0.1,0.31,0.62,0.9,0.89,0.83,0.17,0.68,0.55,0.47,0.97,0.95,0.36,0.67,0.82,0.96,0.77,0.83,0.08,0.34,0.52,0.83,0.53,0.32,0.67,0.05,0.69,0.88,0.55,0.28,0.03,0.93,0.48,0.47,0.16,0.4,0.04,0.3,0.53,0.01,0.73,0.67,0.8,0.31,0.41,0.86,1,0.12,0.77,0.51,0.4,0.32,0.61,0.13,0.16,0.17,0.77,0.73,0.83,0.24,0.5,0.2,0.38,0.26,0.71,0.71,0.75,0.5,0.94,0.41,0.82,0.63,0.22,0.55,0.15,0.42,0.81,0.05,0.93,0.46,0.03,0.87,0.66,0.49,0.95,0.86,0.53,0.4,0.09,0.5,0.87,0.55,0.09,0.43,0.17,0.32,0.05,0.41,0.04,0.18,0.68,0.45,0.43,0.46,0.75,0.08,0.57,0.57,0.44,0.47,0.99,0.8,0.34,0.77,0.47,0.29,0.49,0.95,0.46,0.35,0.5,0.56,0.44,0.06,0.92,0.53,0.4,0.47,0.63,0.56,0.16,0.36,0.79,0.19,0.65,0.29,0.82,0.25,0.04,0.67,0.97,0.97,0.47,0.33,0.19,0.31,0.09,0.73,0.01,0.33,0.37,0.36,0.17,0.92,0.94,0.58,0.71,0.07,0.87,0.31,0.33,0.18,0.39,0.93,0.66,0.73,0.12,0.93,0.41,0.91,0.28,0.45,0.91,0.11,0.33,0.66,0.96,0.19,0.02,0.02,0.29,0.57,0.79,0.21,0.6,0.02,0.38,0.41,0.29,0.46,0.36,0.26,0.63,0.52,0.1,0.08,0.63,0.46,0.14,0.29,0.44,0.08,0.94,1,0.01,0.96,0.26,0.87,0.8,0.27,0.55,0.6,0.83,0.52,0.39,0.25,0.44,0.19,0.62,0.75,0.71,0.9,0.5,0.12,0.49,0.85,0.04,0.56,0.72,0.18,0.32,0.23,0.78,0.8,0.31,0.34,0.1,0.37,0.17,0.27,0.34,0.06,0,0.14,0.76,0.71,0.33,0.52,0.52,0.86,0.03,0.92,0.45,0,0.16,0.45,0.45,0.72,0.15,0.4,0,0.07,0.59,0.89,0.35,0.29,0.04,0.48,0.95,0.74,0.94,0.2,1,0.55,0.69,0.55,0.19,0.4,0.41,0.21,0.56,0.34,0.39,0.26,0.97,0.74,1,0.69,0.45,0.15,0.25,0.15,0.86,0.57,0.76,0.79,0.9,0.13,0.75,0.24,0.21,0.14,0.82,0.71,0.62,0.68,0.38,0.31,0.29,0.45,0.87,0.35,0.81,0.9,0.75,0.09,0.12,0.65,0.56,0.39,0.22,0.44,0.52,0.76,0.45,0.9,0.85,0.43,0.37,0.36,0.02,0.05,0.59,0.61,0.11,0.1,0.72,0.52,0.72,0.09,0.52,0.94,0,0.14,0.41,0.1,0.49,1,0.91,0.75,0.46,0.1,0.21,0.04,0.4,0.37,0.77,0.79,0.68,0.02,0.93,0.35,0.13,0.4,0.59,0.35,0.27,0.18,0.43,0.14,0.98,0.33,0.31,0.78,0.14,0.15,0.81,0.47,0.12,0.5,0.66,0.9,0.47,0.98,0.98,0.96,0.61,0.33,0.52,0.47,0.78,0.2,0.6,0.45,0.86,0.73,0.73,0.4,0.55,0.27,0.65,0.66,0.01,0.74,0.09,0.29,0.05,0.24,0.82,0.63,0.58,0.38,0.8,0.79,0.33,0.62,0.73,0.39,0.56,0.72,0.15,0.1,0.39,0.9,0.11,0.73,0.13,0.13,0.48,0.7,0.47,0.51,0.63,0.56,0.03,0.21,0.73,0.36,0.84,0.92,0.18,0.37,0.33,0.63,0.83,0.56,0.83,0.9,0.52,0.43,0.81,0.03,0.36,0.96,0.56,1,0.97,0.22,0.51,0.04,0.68,0.52,0.62,0.8,0.4,0.98,0.12,0.08,0.33,0.76,0.71,0.64,0.86,0.44,0.99,0.6,0.72,0.38,0.47,0.6,0.66,0.61,0.11,0.06,0.96,0.98,0.9,0.6,0.22,0.72,0.98,0.55,0.33,0.5,0.37,0.05,0.36,0.95,0.89,0.54,0.59,0.04,0.05,0.82,0.94,0.93,0.86,0.35,0.51,0.03,0.59,0.77,0.38,0.69,0.43,0.65,0.41,0.7,0.45,0.25,0.58,0.6,0.83,0.97,0.36,0.74,0.2,0.66,0.91,0.62,0.76,0.31,0.06,0.02,0.99,0.43,0.63,0.16,0.22,0.9,0.72,0.49,0.11,0.25,0.18,0.36,0.1,0.82,0.22,0.31,0.2,0.48,0.95,0,0.59,0.34,0.41,0.94,0.83,0.27,0.17,0.38,0.23,0.41,0.32,0.2,0.52,0.76,0.19,0.6,0.77,0.94,0.21,0.03,0.33,0.41,0.65,0.19,0.23,0.31,0.9,0.62,0.34,0.72,0.61,0.76,0.52,0.11,0.42,0.03,0.37,0.07,0.03,0.25,0.21,0.43,0.43,0.42,0.58,0.23,0.47,0.22,0.64,0.78,0.16,0.64,0.06,0.21,0.86,0.85,0.45,0.18,0.48,0.2,0.57,0.81,0.06,0.07,0.37,0.57,0.86,0.39,0.12,0.34,0.3,0.27,0.26,0.72,0.72,0.73,0.21,0.91,0.22,0.51,0.4,0.46,0.95,0.03,0.55,0.28,0.17,0.33,0.54,0.14,0.66,0.6,0.73,0.63,0.84,0.38,0.39,0.2,0.29,0.64,0.54,0.03,0.94,0.08,0.54,0,0.04,0.5,0.61,0.8,0.08,0.62,0.35,0.39,0.33,0.35,0.22,0.23,0.11,0.38,0.33,0.55,0.22,0.08,0.6,0.33,0.65,0.13,0.15,0.25,0.2,0.69,0.75,0.29,0.91,0.48,0.55,0.77,0.05,0.73,0.47,0.54,0.17,0.95,0.53,0.01,0.73,0.29,0.34,0.11,0.1,0.38,0.26,0.28,0.01,0.26,0.89,0.31,0.91,0.93,0.08,0.85,0.45,0.58,0.26,0.93,0.93,0.41,0.73,0.04,0.39,0.9,0.23,0.41,0.78,0.43,0.93,0.67,0.47,0.54,0.99,0.57,0.15,0.24,0.1,0.97,0.32,0.4,0.65,0.35,0.09,0.69,0.81,0.05,0.63,0.03,0.41,0.88,0.97,0.85,0.46,0.46,0.34,0.76,0.31,0.4,0.09,0.28,0.4,0.59,0.21,0.64,0.12,0.48,0.75,0.41,0.26,0.15,0.65,0.44,1,0.8,0.33,0.68,0.38,0.27,0.79,0.09,0.64,0.82,0.78,0.91,0.08,0.5,0.19,0.51,0.04,0.8,0.22,0.84,0.41,0.75,0.35,0.69,0.39,0.46,0.39,0,0.79,0.53,0.03,0.41,0.2,1,0.6,0.67,0.29,0.81,0.06,0.03,0.01,0.53,0.48,0.17,0.6,0.23,0.55,0.18,0.06,0.9,0.04,0.26,0.52,0.04,0.53,0.19,0.44,0.27,0.52,0.94,0.88,0.55,0.36,0.31,0.87,0.03,0.72,0.99,0.62,0.45,0.83,0.8,0.3,0.24,0.7,0.04,0.78,0.48,0.76,0.69,0.82,0.37,0.41,0.45,0.14,0.56,0.76,0.28,0.8,0.21,0.46,0.6,0.86,0.1,0.24,0.98,0.94,0.75,0.81,0.27,0.96';
const selectType = ref(1);
const queryString = ref(vector);
const topk=ref(50);
const isEmbedding = ref(false);
const knowledge=ref<ApiKnowledgeManagement.Knowledge | null>(null);
const vectorCount = ref(0);
function setQuery(value:number) {
    selectType.value=value
		if(value ==2 || isEmbedding){
        queryString.value='';
		}
        else {
        queryString.value=vector;
		}
}
const tableData = ref<KnowledgeManagement.ScoredVector[]>([]);
function setTableData(data: KnowledgeManagement.ScoredVector[]) {
  tableData.value = data;
}
async function getKnowledge(){
    startLoading();
    console.log(route.query)
    if(route.query.knowledgeId != null){
    const { data } =await  fetchQueryKnowledge(Number(route.query.knowledgeId));
    if (data) {
            knowledge.value= data;
            nameSpaceString.value=data.nameSpace;
            indexStats();
            endLoading();
    }
  }
}
async function indexStats() {
  const {data} = await fetchDescribeIndexStats(Number(route.query.knowledgeId));
  if(data){
    vectorCount.value=data.totalVectorCount;

    for (const [key] of Object.entries(data.namespaces)) {
      const option = {
        label: key,
        value: key,
        disabled: false
      };

      nameSpaceOptions.push(option);
    }
    if(nameSpaceString.value === null) {
      nameSpaceString.value = nameSpaceOptions[0].value;
    }
    getTableData();
  }
}
async function getTableData() {
  startLoading();
  let id= null;
  let vector=null;
  let query= null;
  if(isEmbedding.value == true){
    query=queryString.value;
  }
  else if(selectType.value==2){
    id=queryString.value;
  }else {
    vector=queryString.value.split(",").map(parseFloat);
  }
  console.log(query)
  if(isEmbedding.value == true){
    const { data } = await fetchEmbaddingVectorList({knowledgeId: Number(route.query.knowledgeId),filter: {Q: query},topK:topk.value,namespace:nameSpaceString.value,includeValues:true,includeMetadata:true});
    if (data) {
      setTimeout(() => {
        console.log(data);
        setTableData(data);
        endLoading();
      }, 1000);
    }
  }
  else{

  const { data } = await fetchVectorList({knowledgeId: Number(route.query.knowledgeId),id,vector,topK:topk.value,namespace:nameSpaceString.value,includeValues:true,includeMetadata:true});
  if (data) {
    setTimeout(() => {
        console.log(data);
      setTableData(data);
      endLoading();
    }, 1000);
  }
  }

}
const columns: Ref<DataTableColumns<KnowledgeManagement.ScoredVector>> = ref([
  {
    type: 'selection',
    align: 'center'
  },
  {
    key: 'index',
    title: '序号',
    align: 'center',
    sorter: (row1, row2) => row1.score - row2.score
  },
  {
    key: 'score',
    title: 'SCORE',
    align: 'center'
  },
  {
    key: 'id',
    title: 'ID',
    align: 'center'
  },
  {
    key: 'values',
    title: 'VALUES',
    resizable: true,
    align: 'center',
    ellipsis: true
  },
  {
    key: 'metadata',
    title: 'METADATA',
    align: 'center',
    render: (rowData) => {
      const josn = JSON.stringify(rowData.metadata);
      return `${josn}`;
    }
  },
  {
    key: 'index',
    title: '操作',
    align: 'center',
    render: row => {
      return (
          <NSpace justify={'center'}>
            <NButton size={'small'} onClick={() => handleEditTable(row.index)}>
              编辑
            </NButton>
            <NPopconfirm onPositiveClick={() => handleDeleteTable(row.index)}>
              {{
                default: () => '确认删除',
                trigger: () => <NButton size={'small'}>删除</NButton>
              }}
            </NPopconfirm>
          </NSpace>
      );
    }
  }
]) as Ref<DataTableColumns<KnowledgeManagement.ScoredVector>>;

const modalType = ref<ModalType>('add');

function setModalType(type: ModalType) {
  modalType.value = type;
}

const editData = ref<KnowledgeManagement.ScoredVector | null>(null);

function setEditData(data: KnowledgeManagement.ScoredVector | null) {
  editData.value = data;
}

function handleAddTable() {
  openModal();
  setModalType('add');
}

function handleEditTable(rowId: number) {
  const findItem = tableData.value.find(item => item.index === rowId);
  if (findItem) {
    setEditData(findItem);
  }
  setModalType('edit');
  openModal();
}

async function handleDeleteTable(rowId: number) {
  const { data } = await fetchDelUser(rowId);
  if (data) {
    window.$message?.success('删除成功!');
    const idx = tableData.value.findIndex(item => item.index === rowId);
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
    getKnowledge();
}

// 初始化
init();
</script>
