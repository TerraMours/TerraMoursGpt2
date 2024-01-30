<template>
	<div class="flex flex-col w-full h-full">
	<main class="flex-1 overflow-hidden">
	<div id="scrollRef" ref="scrollRef" class="h-full  overflow-y-auto">
		<div
			id="image-wrapper"
			class="w-full max-w-screen-xl m-auto dark:bg-[#101014]"
			:class="[isMobile ? 'p-2' : 'p-4']"
		>
			<template v-if="!chatRecords.length">
				<div class="flex items-center justify-center mt-4 text-center text-neutral-300">
					<SvgIcon icon="ri:bubble-chart-fill" class="mr-2 text-3xl" />
					<span>QQ群：814880639 </span>
				</div>
				<div class="flex items-center justify-center mt-4 text-center text-neutral-300">
					<span>方便获取第一手更新说明和优惠信息</span>
				</div>
			</template>
			<template v-else>
				<div>
					<Message
						v-for="(item, index) of chatRecords"
						:key="index"
						:date-time="item.createDate"
						:text="item.message"
						:inversion="item.role === 'user'"
						:error="item.error"
						:loading="item.loading"
            :counter="submitFooterInputCounter"
						@delete="handleDelete(index)"
					/>
					<div class="sticky bottom-0 left-0 flex justify-center">
						<NButton v-if="loading" type="warning" @click="handleStop">
							<template #icon>
								<SvgIcon icon="ri:stop-circle-line" />
							</template>
							Stop Responding
						</NButton>
					</div>
				</div>
			</template>
		</div>
	</div>
	</main>
		<footer :class="footerClass">
			<div class="w-full max-w-screen-xl m-auto">
        <n-upload v-if="showImage"
            v-model:file-list="fileList"
            :action="uploadUrl"
            :headers="{ Authorization: `Bearer ${localStg.get('token')}` }"
            list-type="image-card"
            :max="1"
            accept="image/png, image/jpeg"
            @finish="handleFinish"
        />
				<div class="flex items-center justify-between space-x-2">
            <NPopselect
                    v-model:value="modelType" :options="modelOptions" trigger="click" scrollable size="large"
                    :on-update:value="(value) => modelType = value"
            >
                <NButton>{{ modelOptions.find(i => i.value === modelType)?.label || '请选择模型' }}</NButton>
            </NPopselect>
            <n-button @click="showImage=!showImage">
              图文分析
            </n-button>
						<NInput
							ref="inputRef"
							v-model:value="prompt"
							type="textarea"
							:placeholder="placeholder"
							:autosize="{ minRows: 1, maxRows: isMobile ? 4 : 8 }"
							show-count
							@keypress="handleEnter"
						>
							<template #count="">
								<NSpace :size="[2, 0]">
									<span>token : {{ countTokens(prompt) }}</span>
								</NSpace>
							</template>
						</NInput>
					<NButton type="primary" :disabled="buttonDisabled" @click="ChatConversation">
						<template #icon>
            <span class="dark:text-black">
              <SvgIcon :icon="svgIcon" />
            </span>
						</template>
					</NButton>
				</div>
			</div>
		</footer>
	</div>
</template>
<script setup lang="tsx">
import { useChatState } from '@/store';
import { computed, nextTick, onMounted, ref } from 'vue';
import {apiUrl, fetchChatAPIProcess, fetchChatList, fetchDeleteChatRecord} from '@/service';
import { useBasicLayout } from '@/composables';
import {countTokens, localStg} from '../../../../../utils';
import Message from '../message/index.vue';
import { t } from '@/locales';
import {UploadFileInfo} from "naive-ui";
let scrollRef =ref<HTMLElement | null>(null)
const { isMobile } = useBasicLayout()
const chatStore = useChatState();
const page = ref(1)
const pageSize = ref(10)
const chatRecords = ref<ApiGptManagement.Chat[]>([])
const prompt = ref<string>('')
const modelType = ref<string>('gpt-3.5-turbo')
const loading = ref<boolean>(false)
const showImage=ref<boolean>(false)
const usingContext = ref(10)
const buttonDisabled = computed(() => {
	return loading.value || !prompt.value || prompt.value.trim() === ''
})
//上传图片
const fileList = ref<UploadFileInfo[]>();
const uploadUrl = `${apiUrl}/api/v1/Product/UploadProductImage`;
const handleFinish = ({ file, event }: { file: UploadFileInfo; event?: ProgressEvent }) => {
  const res = JSON.parse((event?.target as XMLHttpRequest).response);
  file.url = `${apiUrl}${res.data}`;
  return file;
};
// interface Emits {
//     (e: 'addConversation', param:ApiGptManagement.Conversations): void;
// }
// const emit = defineEmits<Emits>();s
function refreshVue() {
    console.log('ceshi')
    page.value=1;
    pageSize.value=10;
    getChatList();
}
const handleEnter = (event: KeyboardEvent) => {
    if (!isMobile.value) {
        if (event.key === 'Enter' && !event.shiftKey) {
            event.preventDefault()
            ChatConversation()
        }
    }
    else {
        if (event.key === 'Enter' && event.ctrlKey) {
            event.preventDefault()
            ChatConversation()
        }
    }
}
const modelOptions: Array<{ label: string; value: string; length: number;disabled: boolean }> = [
    { label: 'ChatGpt', value: 'ChatGpt', length: 4000, disabled: true },
    { label: 'gpt-3.5-turbo', value: 'gpt-3.5-turbo', length: 2000, disabled: false },
    { label: 'gpt-3.5-turbo-16k', value: 'gpt-3.5-turbo-16k', length: 4000, disabled: false },
    { label: 'gpt-4', value: 'gpt-4', length: 4000, disabled: false },
    { label: 'gpt-4-vision-preview', value: 'gpt-4-vision-preview',length: 4000,disabled:false },
    { label: 'chatGLM', value: 'chatGLM', length: 4000, disabled: true },
    { label: 'chatGLM', value: 'ChatGLM', length: 4000, disabled: false },
    { label: 'chatglm3_turbo', value: 'chatglm3_turbo',length: 4000,disabled:false },
    { label: '文心千帆', value: '文心千帆', length: 4000, disabled: true },
    { label: 'ERNIE_Bot_4', value: 'completions_pro', length: 4000, disabled: false },
    { label: 'ERNIE_Bot_8K', value: 'ernie_bot_8k', length: 4000, disabled: false },
    { label: 'ERNIE_Bot', value: 'completions', length: 4000, disabled: false },
    { label: 'ERNIE_Bot_turbo', value: 'eb-instant', length: 4000, disabled: false },
    { label: 'ERNIE_Bot_turbo_AI', value: 'ai_apaas', length: 4000, disabled: false },
    { label: 'BLOOMZ_7B', value: 'bloomz_7b1', length: 4000, disabled: false },
    { label: 'Qianfan_BLOOMZ_7B_compressed', value: 'qianfan_bloomz_7b_compressed', length: 4000, disabled: false },
    { label: 'Llama_2_7b_chat', value: 'llama_2_7b', length: 4000, disabled: false },
    { label: 'Llama_2_13b_chat', value: 'llama_2_13b', length: 4000, disabled: false },
    { label: 'Llama_2_70b_chat', value: 'llama_2_70b', length: 4000, disabled: false },
    { label: 'Qianfan_Chinese_Llama_2_7B', value: 'qianfan_chinese_llama_2_7b', length: 4000, disabled: false },
    { label: 'Qianfan_Chinese_Llama_2_13B', value: 'qianfan_chinese_llama_2_13b', length: 4000, disabled: false },
    { label: 'ChatGLM2_6B_32K', value: 'chatglm2_6b_32k', length: 4000, disabled: false },
    { label: 'XuanYuan_70B_Chat_4bit', value: 'xuanyuan_70b_chat', length: 4000, disabled: false },
    { label: 'AquilaChat_7B', value: 'aquilachat_7b', length: 4000, disabled: false },
    { label: '同义千问', value: '同义千问', length: 4000, disabled: true },
    { label: 'qwen-max', value: 'qwen-max', length: 4000, disabled: false },
    { label: 'qwen-turbo', value: 'qwen-turbo', length: 4000, disabled: false },
    { label: 'qwen-plus', value: 'qwen-plus', length: 4000, disabled: false },

]
// 计算每一种模式下可以输入的字符数
 const submitFooterInputCounter = computed(() => modelOptions.find(i => i.value === modelType.value)?.length || 0)
// 添加请求终断对象
const controller = ref(new AbortController())
const ganNewController = () => {
	controller.value = new AbortController()
	return controller.value
}
const footerClass = computed(() => {
	if (isMobile)
		return ['sticky', 'left-0', 'bottom-0', 'right-0', 'p-2', 'pr-3', 'overflow-hidden']

	return ['p-4']
})
const svgIcon = ref('ri:send-plane-fill')

const placeholder = computed(() => {
	if (isMobile)
		return t('message.chat.placeholderMobile')
	return t('message.chat.placeholder')
})
async function getChatList() {
	console.log("加载当前会话id"+chatStore.active);
	const { data } = await fetchChatList(chatStore.active,null, page.value, pageSize.value);
	if (data) {
		chatRecords.value = data.items;
    scrollToBottom();
	}
}
// 点击删除指定记录
function handleDelete(index: number) {
	if (loading.value)
		return

	window.$dialog?.warning({
		title: t('message.chat.deleteMessage'),
		content: t('message.chat.deleteMessageConfirm'),
		positiveText: t('common.yes'),
		negativeText: t('common.no'),
		onPositiveClick: async () => {
			const selectedItem = chatRecords.value[index]
			const data = await fetchDeleteChatRecord(selectedItem.chatRecordId)
			if (data)
				chatRecords.value.splice(index, 1)
		},
	})
}

// 结束请求
function handleStop() {
	if (loading.value) {
		controller.value.abort()
		loading.value = false
		// 刷新loading状态
		chatRecords.value.forEach(m => m.loading = false)
	}
}
function handleScroll(event: Event) {
	const target = event.target as HTMLElement
	if (target.scrollTop === 0)
		slideLoading()
}
/**
 *  滑动加载
 */
async function slideLoading() {
	const { data } = await fetchChatList(chatStore.active, null,page.value + 1, pageSize.value)
	if (!(data) || data.items != null && data.items.length > 0) {
		chatRecords.value = [...data!.items, ...chatRecords.value]
		page.value = page.value + 1
	}
}

// 发起对话
async function ChatConversation() {
  console.log("tupian:"+fileList)
  console.log("tupian:"+fileList.value)
	try {
		const askMessage = prompt.value
		// 防止二次点击按钮重复发起
		if (loading.value)
			return
		if (!askMessage || askMessage.trim() === '')
			return
		const askRecord:ApiGptManagement.Chat = {
			chatRecordId: 0,
			conversationId: 0,
			role: 'user',
			message:askMessage,
			userId: 0,
			userName: '',
			createDate: new Date(),
			promptTokens: 0,
			completionTokens: 0,
			totalTokens: 0,
			error: false,
			loading: false,
			model: '',
			modelType: 'string',
		}
		// 添加用户输出聊天记录
		chatRecords.value.push(askRecord)
		const newRecord: ApiGptManagement.Chat = {
			chatRecordId: 0,
			conversationId: 0,
			role: 'assistant',
			message:askMessage,
			userId: 0,
			userName: '',
			createDate: new Date(),
			promptTokens: 0,
			completionTokens: 0,
			totalTokens: 0,
			error: false,
			loading: true,
			model: '',
			modelType: 'string',
		}
		chatRecords.value.push(newRecord)
		// 发起新会话拉到最底部
		scrollToBottom()
		loading.value = true
		// 调用接口后更新内容
		const index = chatRecords.value.length - 1
		try {
			const fetchChatAPIOnce = async () => {
				await fetchChatAPIProcess({
					conversationId: chatStore.active,
					model:( fileList.value !== undefined && fileList.value[0] !== null && fileList.value[0] !== undefined) ? 'gpt-4-vision-preview' :  modelType.value,
					modelType: 0,
					prompt: askMessage,
					contextCount: !usingContext.value ? 0 : null,
          fileUrl: ( fileList.value !== undefined && fileList.value[0] !== null && fileList.value[0] !== undefined) ?fileList.value[0].url : null,

					// 传入signal 代表此请求可控
					signal: ganNewController().signal,
					onDownloadProgress: ({ event }) => {
						const xhr = event.target
						const { responseText } = xhr
						// Always process the final line
						const lastIndex = responseText.lastIndexOf('\n', responseText.length - 2)
						let chunk = responseText
						if (lastIndex !== -1)
							chunk = responseText.substring(lastIndex)
						try {
							const resdata = JSON.parse(chunk)
							if (resdata.code !== 200) {
								chatRecords.value[index].message = resdata.message
								chatRecords.value[index].loading = false
							}
							else {
								chatRecords.value[index].loading = true
								// 更新新增记录的message字段的值
								chatRecords.value[index].message = resdata.data.Message ?? ''
								if (chatStore.active === 0)
									chatStore.setActive( resdata.data.ConversationId);
							}
							scrollToBottomIfAtBottom()
						}
						catch (error: any) {
							// chatRecords.value[index].message = error.toString()
							// chatRecords.value[index].loading = false
						}
					},
				})
				// 重置当前条目的loading状态
				chatRecords.value[index].loading = false
				prompt.value = ''
        fileList.value=[]
			}
			await fetchChatAPIOnce()
		}
		catch (error: any) {
			chatRecords.value[index].message = error.toString()
			chatRecords.value[index].loading = false
		}
	}
	finally {
		loading.value = false
	}
}

const scrollToBottom = async () => {
	await nextTick()
	if (scrollRef && scrollRef.value) {
		scrollRef.value.scrollTop = scrollRef.value.scrollHeight;
	}
}

// const scrollToTop = async () => {
// 	await nextTick()
// 	if (scrollRef)
// 		scrollRef.value.scrollTop = 0
// }

const scrollToBottomIfAtBottom = async () => {

	await nextTick()
	if (scrollRef && scrollRef.value) {
		const threshold = 100 // 阈值，表示滚动条到底部的距离阈值
		const distanceToBottom = scrollRef.value.scrollHeight - scrollRef.value.scrollTop - scrollRef.value.clientHeight
		if (distanceToBottom <= threshold)
			scrollRef.value.scrollTop = scrollRef.value.scrollHeight
	}
}

onMounted(() => {
    console.log('刷新')
  scrollRef.value=document.getElementById('scrollRef');
  if (scrollRef.value)
  scrollRef.value.addEventListener('scroll', handleScroll)
	getChatList();
});

defineExpose({ refreshVue });
</script>
