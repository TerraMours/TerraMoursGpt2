<template>
  <div class="h-full overflow-y-auto">
    <n-card  :bordered="false" class="rounded-16px shadow-sm ">
      <n-tabs type="segment">
        <n-tab-pane v-for="(tab, index) in getTabList" :key="index" :name="tab" :tab="tab">
          <template v-for="(good) in getGoodsByType(tab)">
            <n-badge :value="good.discount" style="margin-right: 10px;max-width: 200px; justify-content: space-between;">
              <n-card :title="good.name" shadow="hover" hoverable style="max-width: 300px;">
                <template #cover>
                  <n-image  width="200" height="200" :src="good.imagePath as string" :intersection-observer-options="{ root: '#image-scroll-container' }" />
                </template>
                <n-statistic tabular-nums>
                  <n-number-animation :from="0.0" :to="good.price" :precision="1" />
                  <template #prefix>
                    ￥
                  </template>
                </n-statistic>
                <div style="display: flex; justify-content: flex-end; margin-top: 24px;">
                  <n-button strong round size="large" type="primary" @click="payModel(good)">
                    <template #icon>
                      <SvgIcon class="text-xl" icon="ri:link" />
                    </template>
                    立即充值
                  </n-button>
                </div>
              </n-card>
            </n-badge>
          </template>
        </n-tab-pane>
      </n-tabs>
    </n-card>

    <n-modal v-model:show="showModal" style="width: 90%; max-width: 600px; " preset="card">
      <n-h1 style="text-align: center">
        扫码支付
      </n-h1>
      <n-h3 style="text-align: center">
        支付方式：请打开支付宝扫码支付！有效期3分钟
      </n-h3>
      <div style="display: flex;justify-content: center;align-items: center;">
        <n-qr-code v-model:value="totpUrl"    color="#409eff" :size = "120"
                   background-color="#F5F5F5"/>
      </div>
      <n-h3 style="text-align: center">
        需要支付金额：{{ goodPrice }}
      </n-h3>
    </n-modal>
  </div>
</template>
<script setup lang="tsx">

import {computed, onMounted, reactive, ref} from "vue";
import {useMessage} from "naive-ui";
import {apiUrl, fetchAllProductList, PreCreate} from "@/service";
import type { HubConnection } from '@microsoft/signalr'
import { HubConnectionBuilder } from '@microsoft/signalr'


const showModal = ref(false);
const goodList = ref<ApiPayManagement.Product[]>([])

const goodPrice = ref(0)
const totpUrl = ref('')
const currentOderId = ref('')
const ms = useMessage()
// signalr
const connection = ref<HubConnection | null>(null)


const getAllGoods = () => goodList.value

const getTabList = computed(() => {
  const typeSet = new Set(goodList.value.map(good => good.categoryName))
  return ['全部', ...Array.from(typeSet)]
})

function getGoodsByType(type: string | '全部'): ApiPayManagement.Product[] {
  if (type === '全部')
    return getAllGoods()

  return goodList.value.filter(good => good.categoryName === type)
}

const getAllProductList = async () => {
  const { data } = await fetchAllProductList();
  if (data) {
    setTimeout(() => {
      goodList.value = data
    }, 1000);
  }

}

const payModel = async (good: ApiPayManagement.Product) => {
  goodPrice.value = good.price
  showModal.value = true

  const { data } = await PreCreate(good.name, good.price, good.description, good.id, good.isVIP, good.vipLevel, good.vipTime)
  if (data != null) {
    totpUrl.value = data.qr_code
    currentOderId.value = data.out_trade_no
    const req = reactive({ outTradeNo: data.out_trade_no, TradeNo: null })
    connection.value?.invoke('QueryPaymentStatus', req)
  }
}

const signalConnect = async () => {
  connection.value = new HubConnectionBuilder().withUrl(`${apiUrl}/Hubs/QueryPaymentStatus`)
      .withAutomaticReconnect().build()
  await connection.value.start()
}
onMounted(async () => {
  getAllProductList()
  await signalConnect()
  connection.value?.on('QueryPaymentStatus', (data: { outTradeNo: string; tradeStatus: string }) => {
    if (data.outTradeNo === currentOderId.value) {
      if (data.tradeStatus === 'TRADE_SUCCESS')
        ms.success(`充值成功！成功成功充值￥${goodPrice.value}`)

      else
        ms.warning('未付款交易超时关闭.请重新购买')

      showModal.value = false
    }
  })
})
</script>
