<template>
  <div class="h-full overflow-hidden">
    <n-card title="Key池管理" :bordered="false" class="rounded-16px shadow-sm">
      <n-space class="pb-12px" justify="space-between">
        <n-space>
          <n-button type="primary" @click="addItem">
            <icon-ic-round-plus class="mr-4px text-20px" />
            新增
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
<!--      <n-data-table :columns="columns" :data="tableData" :loading="loading" :pagination="pagination" />-->
			<n-data-table
				:key="(row) => row.key"
				:columns="columns"
				:data="keyList"
				:loading="loading"
				:pagination="pagination"
				:on-update:page="handlePageChange"
			/>
<!--      <table-action-modal v-model:visible="visible" :type="modalType" :edit-data="editData" @updateDataTable="getTableData"/>-->
    </n-card>
  </div>
</template>

<script setup lang="tsx">
import {defineComponent, h, nextTick, reactive, ref} from 'vue';
import type { Ref } from 'vue';
import {NButton, NInput, NPopconfirm, NSpace} from 'naive-ui';
import type { DataTableColumns, PaginationProps } from 'naive-ui';
import {fetchKeyOptionList, fetchDeleteKeyOptions, GetOpenAIOptions} from '@/service';
import { useBoolean, useLoading } from '@/hooks';
import TableActionModal from './components/table-action-modal.vue';
import type { ModalType } from './components/table-action-modal.vue';
const { loading, startLoading, endLoading } = useLoading(false);
const { bool: visible, setTrue: openModal } = useBoolean();

const Options: { label: string; value: string,disabled: boolean,type:number}[] = [
	{ label: 'ChatGpt', value: 'ChatGpt',disabled:true,type:1 },
	{ label: 'gpt-3.5-turbo', value: 'gpt-3.5-turbo',disabled:false,type:1 },
	{ label: 'gpt-3.5-turbo-16k',value: 'gpt-3.5-turbo-16k',disabled:false,type:1 },
	{ label: 'gpt-4', value: 'gpt-4',disabled:false ,type:1},
	{ label: 'gpt-4-vision-preview', value: 'gpt-4-vision-preview',disabled:false ,type:1},
	{ label: 'ChatGLM', value: 'ChatGLM',disabled:true ,type:1},
	{ label: 'ChatGLM', value: 'ChatGLM',disabled:false,type:1 },
	{ label: 'chatglm3_turbo', value: 'chatglm3_turbo',disabled:false ,type:1},
	{ label: '文心千帆', value: '文心千帆',disabled:true,type:2 },
	{ label: 'ERNIE_Bot_4', value: 'completions_pro',disabled:false ,type:2},
	{ label: 'ERNIE_Bot_8K', value: 'ernie_bot_8k',disabled:false ,type:2},
	{ label: 'ERNIE_Bot', value: 'completions',disabled:false,type:2 },
	{ label: 'ERNIE_Bot_turbo', value: 'eb-instant',disabled:false,type:2 },
	{ label: 'ERNIE_Bot_turbo_AI', value: 'ai_apaas',disabled:false ,type:2},
	{ label: 'BLOOMZ_7B', value: 'bloomz_7b1',disabled:false ,type:2},
	{ label: 'Qianfan_BLOOMZ_7B_compressed', value: 'qianfan_bloomz_7b_compressed',disabled:false,type:2 },
	{ label: 'Llama_2_7b_chat', value: 'llama_2_7b',disabled:false,type:2 },
	{ label: 'Llama_2_13b_chat', value: 'llama_2_13b',disabled:false ,type:2},
	{ label: 'Llama_2_70b_chat', value: 'llama_2_70b',disabled:false ,type:2},
	{ label: 'Qianfan_Chinese_Llama_2_7B', value: 'qianfan_chinese_llama_2_7b',disabled:false ,type:2},
	{ label: 'Qianfan_Chinese_Llama_2_13B', value: 'qianfan_chinese_llama_2_13b',disabled:false ,type:2},
	{ label: 'ChatGLM2_6B_32K', value: 'chatglm2_6b_32k',disabled:false ,type:2},
	{ label: 'XuanYuan_70B_Chat_4bit', value: 'xuanyuan_70b_chat',disabled:false ,type:2},
	{ label: 'AquilaChat_7B', value: 'aquilachat_7b',disabled:false ,type:2},
	{ label: 'Stable-Diffusion-XL', value: 'Stable-Diffusion-XL',disabled:false ,type:2},
	{ label: '同义千问', value: '同义千问',disabled:true,type:3},
	{ label: 'qwen-max', value: 'qwen-max',disabled:false ,type:3},
	{ label: 'qwen-turbo', value: 'qwen-turbo',disabled:false ,type:3},
	{ label: 'qwen-plus', value: 'qwen-plus',disabled:false,type:3 },
	{ label: 'stable diffusion', value: 'stable diffusion',disabled:true ,type:4},
	{ label: '二次元', value: 'SD2D',disabled:false ,type:4},
	{ label: '真人', value: 'SD3D',disabled:false ,type:4},
]
const ModelOptions: { label: string; value: number,disabled: boolean}[] = [
	{ label: 'OpenAI', value: 1,disabled:false },
	{ label: '文心千帆', value: 2,disabled:false },
	{ label: '通义千问',value: 3,disabled:false },
	{ label: 'stable diffusion', value: 4,disabled:false },
	{ label: 'Midjornay', value: 5,disabled:false },
]

const typeOptions = (type:number) => {
  return Options.filter(m=>m.type === type);
}
const columns: Ref<DataTableColumns<ApiGptManagement.AKeyOption>> = ref([
  {
    key: 'type',
    title: '类型',
    width: 20,
    align: 'center',
		render (row) {
			return(
				<n-select v-model:value={row.type} size="medium" options={ModelOptions} />
			);
		}
  },
  {
    key: 'key',
    title: 'key',
    width: 200,
    align: 'center',
    render (row) {
      return h(ShowOrEdit, {
        value: row.key,
        onUpdateValue (v) {
          row.key = v
        }
      })
    }
  },
  {
    key: 'baseUrl',
    title: '代理地址',
    width: 200,
    align: 'center',
    render (row) {
      return h(ShowOrEdit, {
        value: row.key,
        onUpdateValue (v) {
          row.key = v
        }
      })
    }
  },
  {
    key: 'modelTypes',
    title: '可用模型',
    width: 20,
    align: 'center',
    render (row) {
      return(
          <n-select v-model:value={row.modelTypes}   multiple options={typeOptions(row.type)} />
      );
    }
  },
  {
    key: 'actions',
    title: '操作',
    width: 20,
    align: 'center',
    render: row => {
      return (
        <NSpace justify={'center'}>
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
]) as Ref<DataTableColumns<ApiGptManagement.AKeyOption>>;



//key池
const keyList =ref< ApiGptManagement.AKeyOption[]>([]);
const page = ref(1)
async function GetOpenAIOption() {
	const { data } = await GetOpenAIOptions();
	if(data !=null && data.openAI !=null){
		keyList.value=data.openAI.keyList;
	}
}

const getDataIndex = (key) => {
	return keyList.value.findIndex((item) => item.key === key)
}
const handlePageChange = (curPage) => {
	keyList.value = curPage
}

const addItem = () => {
  keyList.value.push({key:'',baseUrl:'',isEnable:true,modelTypes:[],type:1});
};
async function handleDeleteTable(index: number) {
  keyList.value.splice(index, 1);
}

const ShowOrEdit = defineComponent({
  props: {
    value: {
      type: [String, Number],
      required: true
    },
    onUpdateValue: {
      type: [Function, Array],
      required: true
    }
  },
  setup(props) {
    const isEdit = ref(false);
    const inputRef = ref<HTMLInputElement | null>(null);
    const inputValue = ref(props.value);

    function handleOnClick() {
      isEdit.value = true;
      nextTick(() => {
        inputRef.value?.focus();
      });
    }

    function handleChange() {
      props.onUpdateValue(inputValue.value);
      isEdit.value = false;
    }

    return () => (
        <div style="min-height: 22px" onClick={handleOnClick}>
          {isEdit.value ? (
              <NInput
                  ref={inputRef}
                  value={inputValue.value}
                  onUpdateValue={(v: string | number) => {
                    inputValue.value = v;
                  }}
                  onChange={handleChange}
                  onBlur={handleChange}
              />
          ) : (
              props.value.toString()
          )}
        </div>
    );
  }
});

function init() {
	GetOpenAIOption();
}

// 初始化
init();
</script>

<style scoped></style>
