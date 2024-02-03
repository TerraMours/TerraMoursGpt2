<template>
  <n-grid cols="s:1 m:2 l:5" responsive="screen" :x-gap="16" :y-gap="16">
    <n-grid-item v-for="item in analysisData">
      <gradient-bg class="h-120px" :start-color="item.colors[0]" :end-color="item.colors[1]">
        <h3 class="text-16px">{{ item.title }}</h3>
        <div class="flex justify-between pt-12px">
          <svg-icon v-if="item.lastValue>0" icon="icon-park-outline:trending-up" class="text-32px" />
          <svg-icon v-else-if="item.lastValue<0" icon="icon-park-outline:trending-down" class="text-32px" />
          <svg-icon v-else icon="icon-park-outline:transfer-data" class="text-32px" />
          <count-to
            :prefix="item.unit"
            :start-value="1"
            :end-value="item.value"
            class="text-32px text-white dark:text-dark"
          />
        </div>
        <div  class="flex justify-end" v-if="item.lastValue !=0">
          <h3 class="text-15px">较昨日</h3>
          <svg-icon v-if="item.lastValue>0" icon="icon-park-outline:trending-up" class="text-15px" />
          <svg-icon v-else-if="item.lastValue<0" icon="icon-park-outline:trending-down" class="text-15px" />
          <svg-icon v-else icon="icon-park-outline:transfer-data" class="text-15px" />
          <count-to
              :start-value="1"
              :end-value="item.lastValue"
              class="text-15px text-white dark:text-dark "
          />
        </div>
      </gradient-bg>
    </n-grid-item>
  </n-grid>
</template>

<script setup lang="ts">
import { GradientBg } from './components';
import { fetchTotalAnalysis } from '@/service';
import {onMounted, ref} from "vue";
defineOptions({ name: 'DashboardAnalysisDataCard' });

const analysisData=ref<CardData[]>([]);
async function getTotalAnalysis() {
  const { data } = await fetchTotalAnalysis(null,null,null);
  if(data) {
    analysisData.value = data.map((item, index) => ({
      ...cardData[index],
      title: item.key,
      value: item.total,
      lastValue: item.total - item.lastTotal
    }));
  }
}
interface CardData {
  id: string;
  title: string;
  value: number;
  lastValue:number;
  unit: string;
  colors: [string, string];
  icon: string;
}

const cardData: CardData[] = [
  {
    id: 'visit',
    title: '用户总数',
    value: 1000000,
    lastValue:1000000,
    unit: '',
    colors: ['#ec4786', '#b955a4'],
    icon: 'ant-design:bar-chart-outlined'
  },
  {
    id: 'download',
    title: '下载数',
    value: 666666,
    lastValue: 666666,
    unit: '',
    colors: ['#56cdf3', '#719de3'],
    icon: 'carbon:document-download'
  },
  {
    id: 'trade',
    title: '成交数',
    value: 999999,
    lastValue: 999999,
    unit: '',
    colors: ['#fcbc25', '#f68057'],
    icon: 'ant-design:trademark-circle-outlined'
  },
  {
    id: 'trade',
    title: '成交数',
    value: 999999,
    lastValue: 999999,
    unit: '',
    colors: ['#fcbc25', '#f68057'],
    icon: 'ant-design:trademark-circle-outlined'
  },
  {
    id: 'amount',
    title: '成交额',
    value: 234567.89,
    lastValue: 234567.89,
    unit: '￥',
    colors: ['#865ec0', '#5144b4'],
    icon: 'ant-design:money-collect-outlined'
  },
];
onMounted(() => {
  getTotalAnalysis();
});
</script>

<style scoped></style>
