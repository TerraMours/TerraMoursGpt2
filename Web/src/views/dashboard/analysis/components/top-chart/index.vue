<template>
  <n-grid :x-gap="16" :y-gap="16" :item-responsive="true">
    <n-grid-item span="0:24 640:24 1024:8">
      <n-card :bordered="false" class="rounded-16px shadow-sm">
        <n-select class="w-120px ml-auto" v-model:value="saleSelectType" :options="pieModelOptions" @update:value="getSaleAnalysis"/>
        <div class="w-full h-360px py-12px">
          <h3 class="text-16px font-bold">销售统计</h3>
          <p class="text-#aaa">统计数据</p>
          <div v-for="item in saleData">
            <h3 class="pt-32px text-24px font-bold">
              <count-to v-if="item.key =='销售额'" prefix="$" :start-value="0" :end-value="item.total" />
              <count-to v-else  :start-value="0" :end-value="item.total" />
            </h3>
            <p class="text-#aaa" >{{ item.key }}</p>
          </div>
        </div>
      </n-card>
    </n-grid-item>
    <n-grid-item span="0:24 640:24 1024:8">
      <n-card :bordered="false" class="rounded-16px shadow-sm">
        <n-select class="w-120px ml-auto" v-model:value="askSelectType" :options="Options" @update:value="askHandleUpdateValue"/>
        <div ref="lineRef" class="w-full h-360px"></div>
      </n-card>
    </n-grid-item>
    <n-grid-item span="0:24 640:24 1024:8">
      <n-card :bordered="false" class="rounded-16px shadow-sm">
        <div style="display: flex; justify-content: space-between;">
          <n-select class="w-120px" v-model:value="analysisSelectType" :options="analysisTypeOptions" @update:value="getModelList"/>
          <n-select class="w-120px" v-model:value="modelSelectType" :options="pieModelOptions" @update:value="getModelList"/>
        </div>
        <div ref="pieRef" class="w-full h-360px"></div>
      </n-card>
    </n-grid-item>
    <n-grid-item span="0:24 640:24 1024:8">
      <n-card :bordered="false" class="rounded-16px shadow-sm">
        <n-select class="w-120px ml-auto" v-model:value="tokenSelectType" :options="Options" @update:value="getTokenList"/>
        <div ref="tokenRef" class="w-full h-360px"></div>
      </n-card>
    </n-grid-item>
    <n-grid-item span="0:24 640:24 1024:8">
      <n-card :bordered="false" class="rounded-16px shadow-sm">
        <n-select class="w-120px ml-auto" v-model:value="useSelectType" :options="Options" @update:value="getUseList"/>
        <div ref="useRef" class="w-full h-360px"></div>
      </n-card>
    </n-grid-item>
    <n-grid-item span="0:24 640:24 1024:8">
      <n-card :bordered="false" class="rounded-16px shadow-sm">
        <n-select class="w-120px ml-auto" v-model:value="saleMoneySelectType" :options="Options" @update:value="getSaleMoneyList"/>
        <div ref="saleMoneyRef" class="w-full h-360px"></div>
      </n-card>
    </n-grid-item>
  </n-grid>
</template>

<script setup lang="ts">
import {onMounted, ref, watchEffect} from 'vue';
import type { Ref } from 'vue';
import { type ECOption, useEcharts } from '@/composables';
import {fetchAllAnalysisList, fetchAnalysisList, fetchPieAnalysisList, fetchSaleAnalysis} from '@/service';
import AllAnalysis = ApiAnalysisManagement.AllAnalysis;
import TotalAnalysis = ApiAnalysisManagement.TotalAnalysis;

const askSelectType=ref(1);
const tokenSelectType=ref(1);
const useSelectType=ref(1);
const saleMoneySelectType=ref(1);
const modelSelectType=ref(1);
const analysisSelectType=ref(6);
const saleSelectType=ref(1);
const Options: { label: string; value: number }[] = [
  { label: '当天', value: 1 },
  { label: '按月',value: 4 },
  { label: '按天', value: 0 },
]
const pieModelOptions: { label: string; value: number }[] = [
  { label: '当天', value: 1 },
  { label: '本月',value: 4 },
  { label: '今年', value: 5 },
  { label: '至今', value: 0 },
]

const analysisTypeOptions: { label: string; value: number }[] = [
  { label: '提问次数', value: 0 },
  { label: '图片生成数',value: 2 },
  { label: 'Token消耗量', value: 6 },
]


defineOptions({ name: 'DashboardAnalysisTopCard' });
const analysisData=ref<AllAnalysis[]>([]);
const tokenData=ref<TotalAnalysis[]>([]);
const useData=ref<TotalAnalysis[]>([]);
const saleMoneyData=ref<TotalAnalysis[]>([]);
const pieData=ref<TotalAnalysis[]>([]);
const saleData=ref<TotalAnalysis[]>([]);
const lineOptions = ref<ECOption>({
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'cross',
      label: {
        backgroundColor: '#6a7985'
      }
    }
  },
  legend: {
    data: ['提问量', '图片量']
  },
  grid: {
    left: '3%',
    right: '4%',
    bottom: '3%',
    containLabel: true
  },
  xAxis: {
      type: 'category',
      boundaryGap: false,
      data: ['06:00', '08:00', '10:00', '12:00', '14:00', '16:00', '18:00', '20:00', '22:00', '24:00']
    }
  ,
  yAxis: [
    {
      type: 'value'
    }
  ],
  series: [
    {
      color: '#8e9dff',
      name: '提问量',
      type: 'line',
      smooth: true,
      stack: 'Total',
      areaStyle: {
        color: {
          type: 'linear',
          x: 0,
          y: 0,
          x2: 0,
          y2: 1,
          colorStops: [
            {
              offset: 0.25,
              color: '#8e9dff'
            },
            {
              offset: 1,
              color: '#fff'
            }
          ]
        }
      },
      emphasis: {
        focus: 'series'
      },
      data: [4623, 6145, 6268, 6411, 1890, 4251, 2978, 3880, 3606, 4311]
    },
    {
      color: '#26deca',
      name: '图片量',
      type: 'line',
      smooth: true,
      stack: 'Total',
      areaStyle: {
        color: {
          type: 'linear',
          x: 0,
          y: 0,
          x2: 0,
          y2: 1,
          colorStops: [
            {
              offset: 0.25,
              color: '#26deca'
            },
            {
              offset: 1,
              color: '#fff'
            }
          ]
        }
      },
      emphasis: {
        focus: 'series'
      },
      data: [2208, 2016, 2916, 4512, 8281, 2008, 1963, 2367, 2956, 678]
    }
  ]
}) as Ref<ECOption>;
const { domRef: lineRef } = useEcharts(lineOptions);

const tokenOptions = ref<ECOption>({
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'cross',
      label: {
        backgroundColor: '#6a7985'
      }
    }
  },
  xAxis: {
    type: 'category',
    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
  },
  yAxis: {
    type: 'value'
  },
  legend: {
    data: ['token消耗量']
  },
  series: [
    {
      name: 'token消耗量',
      data: [120, 200, 150, 80, 70, 110, 130],
      type: 'bar',
      color: '#8378ea',
      showBackground: true,
      barGap: 100,
      itemStyle: {
        borderRadius: [40, 40, 0, 0]
      },
      backgroundStyle: {
        color: 'rgba(180, 180, 180, 0.2)'
      }
    }
  ]
}) as Ref<ECOption>;
const { domRef: tokenRef } = useEcharts(tokenOptions);

const useOptions = ref<ECOption>({
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'cross',
      label: {
        backgroundColor: '#6a7985'
      }
    }
  },
  xAxis: {
    type: 'category',
    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
  },
  yAxis: {
    type: 'value'
  },
  legend: {
    data: ['上线人数']
  },
  series: [
    {
      name: '上线人数',
      data: [120, 200, 150, 80, 70, 110, 130],
      type: 'bar',
      color: '#8378ea',
      showBackground: true,
      barGap: 100,
      itemStyle: {
        borderRadius: [40, 40, 0, 0]
      },
      backgroundStyle: {
        color: 'rgba(180, 180, 180, 0.2)'
      }
    }
  ]
}) as Ref<ECOption>;
const { domRef: useRef } = useEcharts(useOptions);
const saleMoneyOptions = ref<ECOption>({
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'cross',
      label: {
        backgroundColor: '#6a7985'
      }
    }
  },
  xAxis: {
    type: 'category',
    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
  },
  yAxis: {
    type: 'value'
  },
  legend: {
    data: ['销售额']
  },
  series: [
    {
      name: '销售额',
      data: [120, 200, 150, 80, 70, 110, 130],
      type: 'bar',
      color: '#8378ea',
      showBackground: true,
      barGap: 100,
      itemStyle: {
        borderRadius: [40, 40, 0, 0]
      },
      backgroundStyle: {
        color: 'rgba(180, 180, 180, 0.2)'
      }
    }
  ]
}) as Ref<ECOption>;
const { domRef: saleMoneyRef } = useEcharts(saleMoneyOptions);

const pieOptions = ref<ECOption>({
  tooltip: {
    trigger: 'item'
  },
  legend: {
    bottom: '1%',
    left: 'center',
    itemStyle: {
      borderWidth: 0
    }
  },
  series: [
    {
      color: ['#5da8ff', '#8e9dff', '#fedc69', '#26deca'],
      name: '模型统计',
      type: 'pie',
      radius: ['45%', '75%'],
      avoidLabelOverlap: false,
      itemStyle: {
        borderRadius: 10,
        borderColor: '#fff',
        borderWidth: 1
      },
      label: {
        show: false,
        position: 'center'
      },
      emphasis: {
        label: {
          show: true,
          fontSize: '12'
        }
      },
      labelLine: {
        show: false
      },
      data: [
        { value: 20, name: '学习' },
        { value: 10, name: '娱乐' },
        { value: 30, name: '工作' },
        { value: 40, name: '休息' }
      ]
    }
  ]
}) as Ref<ECOption>;
const { domRef: pieRef } = useEcharts(pieOptions);

watchEffect(() => {
  if (lineOptions.value.xAxis) {
    (<any>lineOptions.value.xAxis).data = analysisData.value.map(m => m.key);
  }
  if (lineOptions.value.series && Array.isArray(lineOptions.value.series) && lineOptions.value.series.length > 0) {
    lineOptions.value.series[0].data = analysisData.value.map(m => m.askCount);
    lineOptions.value.series[1].data = analysisData.value.map(m => m.imageCount);
  }
  //token
  if (tokenOptions.value.xAxis) {
    (<any>tokenOptions.value.xAxis).data = tokenData.value.map(m => m.key);
  }
  if (tokenOptions.value.series && Array.isArray(tokenOptions.value.series) && tokenOptions.value.series.length > 0) {
    tokenOptions.value.series[0].data = tokenData.value.map(m => m.total);
  }
  //USE
  if (useOptions.value.xAxis) {
    (<any>useOptions.value.xAxis).data = useData.value.map(m => m.key);
  }
  if (useOptions.value.series && Array.isArray(useOptions.value.series) && useOptions.value.series.length > 0) {
    useOptions.value.series[0].data = useData.value.map(m => m.total);
  }
  //saleMoney
  if (saleMoneyOptions.value.xAxis) {
    (<any>saleMoneyOptions.value.xAxis).data = saleMoneyData.value.map(m => m.key);
  }
  if (saleMoneyOptions.value.series && Array.isArray(saleMoneyOptions.value.series) && saleMoneyOptions.value.series.length > 0) {
    saleMoneyOptions.value.series[0].data = saleMoneyData.value.map(m => m.total);
  }
  //pie
  if (pieOptions.value.xAxis) {
    (<any>pieOptions.value.xAxis).data = pieData.value.map(m => m.key);
  }
  if (pieOptions.value.series && Array.isArray(pieOptions.value.series) && pieOptions.value.series.length > 0) {
    pieOptions.value.series[0].data = pieData.value.map(item => ({
      value: item.total,
      name: item.key,
    }));
  }
});
async function getAnalysisList() {
	const { data } = await fetchAllAnalysisList(askSelectType.value, null, null);
	if (data) {
		analysisData.value = data;
	}
}
async function getTokenList() {
  const { data } = await fetchAnalysisList(tokenSelectType.value, null, null,6);
  if (data) {
    tokenData.value = data;
  }
}
async function getUseList() {
  const { data } = await fetchAnalysisList(useSelectType.value, null, null,1);
  if (data) {
    useData.value = data;
  }
}
async function getSaleMoneyList() {
  const { data } = await fetchAnalysisList(saleMoneySelectType.value, null, null,5);
  if (data) {
    saleMoneyData.value = data;
  }
}
async function getModelList() {
  const { data } = await fetchPieAnalysisList(modelSelectType.value, null, null,analysisSelectType.value);
  if (data) {
    pieData.value = data;
  }
}
async function getSaleAnalysis() {
  const { data } = await fetchSaleAnalysis(null, null, null,analysisSelectType.value);
  if (data) {
    saleData.value = data;
  }
}
async function askHandleUpdateValue(){
  await getAnalysisList();
}

onMounted(() => {
  getAnalysisList();
  getTokenList();
   getUseList();
  getSaleMoneyList();
  getModelList();
  getSaleAnalysis();
});
</script>

<style scoped></style>
