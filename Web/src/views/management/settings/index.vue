<template>
    <div class="h-full overflow-y-auto">
      <n-card title="邮件服务设置" :bordered="false" class="rounded-16px shadow-sm ">
        <n-switch v-model:value="updateEmailDisabled" @update:value="handleEmailChange">
          <template #checked>
            点击保存
          </template>
          <template #unchecked>
            点击编辑
          </template>
        </n-switch>
        <n-form ref="formRef" label-placement="left" :label-width="150" :model="formModel" :disabled="!updateEmailDisabled" style="margin-top: 20px;">
        <n-grid :cols="24" :x-gap="18">
          <n-form-item-grid-item :span="12" label="Smtp服务器地址" path="host">
            <n-input v-model:value="formModel.host" />
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="12" label="端口" path="port">
            <n-input-number v-model:value="formModel.port" />
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="12" label="账号" path="senderEmail">
            <n-input v-model:value="formModel.senderEmail" />
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="12" label="密码" path="senderPassword">
            <n-input v-model:value="formModel.senderPassword" />
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="12" label="发件名称" path="senderName">
            <n-input v-model:value="formModel.senderName" />
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="12" label="是否开启ssl协议" path="useSsl">
            <n-switch v-model:value="formModel.useSsl" />
          </n-form-item-grid-item>
        </n-grid>
      </n-form>
      </n-card>
			<n-card title="支付设置" :bordered="false" class="rounded-16px shadow-sm " style="margin-top: 20px;">
					<n-switch v-model:value="updateAlipayDisabled" @update:value="handleAlipayChange">
							<template #checked>
									点击保存
							</template>
							<template #unchecked>
									点击编辑
							</template>
					</n-switch>
					<n-form ref="formRef" label-placement="left" :label-width="150" :model="alipayModel" :disabled="!updateAlipayDisabled" style="margin-top: 20px;">
							<n-grid :cols="24" :x-gap="18">
									<n-form-item-grid-item :span="24" label="商户id" path="host">
											<n-input v-model:value="alipayModel.appId" />
									</n-form-item-grid-item>
									<n-form-item-grid-item  :span="24" label="支付宝公钥" path="port">
											<n-input type="textarea" v-model:value="alipayModel.alipayPublicKey" />
									</n-form-item-grid-item>
									<n-form-item-grid-item :span="24" label="应用私钥" path="senderEmail">
											<n-input type="textarea" v-model:value="alipayModel.appPrivateKey" />
									</n-form-item-grid-item>
							</n-grid>
					</n-form>
			</n-card>
      <n-card title="Gpt设置" :bordered="false" class="rounded-16px shadow-sm " style="margin-top: 20px;">
        <n-switch v-model:value="updateAiDisabled" @update:value="handleGptChange">
          <template #checked>
            点击保存
          </template>
          <template #unchecked>
            点击编辑
          </template>
        </n-switch>
        <n-form  label-placement="left" :label-width="200" :model="openaiModel" :disabled="!updateAiDisabled" style="margin-top: 20px;">
        <n-grid :cols="24" :x-gap="18">
          <n-form-item-grid-item :span="12" label="每1k Token定价" path="TokenPrice">
            <n-input-number v-model:value="openaiModel.tokenPrice"></n-input-number>
          </n-form-item-grid-item>
					<n-form-item-grid-item :span="12" label="新用户赠送金额" path="TokenPrice">
						<n-input-number v-model:value="openaiModel.newUserBalance"></n-input-number>
					</n-form-item-grid-item>
          <n-form-item-grid-item :span="24" label="OpenAI参数配置" path="gpt4Key">
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="MaxTokens最大回复数" path="maxTokens">
            <n-input-number v-model:value="openaiModel.openAI!.maxTokens"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="temperature" path="temperature">
            <n-input-number v-model:value="openaiModel.openAI!.temperature"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="FrequencyPenalty" path="frequencyPenalty">
            <n-input-number v-model:value="openaiModel.openAI!.frequencyPenalty"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="PresencePenalty" path="presencePenalty">
            <n-input-number v-model:value="openaiModel.openAI!.presencePenalty"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="TopP" path="topP">
            <n-input-number v-model:value="openaiModel.openAI!.topP"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="ChatModel默认模型" path="chatModel">
            <n-input v-model:value="openaiModel.openAI!.chatModel"></n-input>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="上下文数量" path="contextCount">
            <n-input-number v-model:value="openaiModel.openAI!.contextCount"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="8" label="最大提问数量" path="maxQuestions">
            <n-input-number v-model:value="openaiModel.openAI!.maxQuestions"></n-input-number>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="24" label="Key池管理：">
            <n-space>
                <n-button attr-type="button" @click="addItem" :disabled="!updateAiDisabled">
                增加
                </n-button>
            </n-space>
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="24"
            v-for="(_,index) in openaiModel.openAI!.keyList"
            :key="index"
            :label="`Key${index + 1}`"
            >
						<n-select v-model:value="openaiModel.openAI!.keyList[ index].type" size="medium" :options="ModelOptions" />
            <n-input v-model:value="openaiModel.openAI!.keyList[ index].key" placeholder="输入key" clearable />
            <n-input v-model:value="openaiModel.openAI!.keyList[ index].baseUrl" placeholder="输入代理地址，不使用代理：https://api.openai.com/" clearable />
            <n-select v-model:value="openaiModel.openAI!.keyList[ index].modelTypes"
              multiple
              :options="Options"
            ></n-select>


            <n-button style="margin-left: 12px" @click="removeItem(index)" :disabled="!updateAiDisabled">
                删除
            </n-button>
         </n-form-item-grid-item>
        </n-grid>
      </n-form>
      </n-card>
      <n-card title="图片服务设置" :bordered="false" class="rounded-16px shadow-sm " style="margin-top: 20px;">
        <n-switch v-model:value="updateImageDisabled" @update:value="handleImageChange">
          <template #checked>
            点击保存
          </template>
          <template #unchecked>
            点击编辑
          </template>
        </n-switch>
        <n-form ref="formRef" label-placement="left" :label-width="150" :model="imageModel" :disabled="!updateImageDisabled" style="margin-top: 20px;">
        <n-grid :cols="24" :x-gap="18">
          <n-form-item-grid-item :span="12" label="图片静态文件地址" path="imagFileBaseUrl">
            <n-input v-model:value="imageModel.imagFileBaseUrl" />
          </n-form-item-grid-item>
          <n-form-item-grid-item :span="12" label="图片定价" path="port">
            <n-input-number v-model:value="imageModel.imagePrice" />
          </n-form-item-grid-item>
        </n-grid>
      </n-form>
      </n-card>
    </div>
  </template>

  <script setup lang="tsx">
  import { ref, onMounted  } from 'vue';
  import {
      GetEmailSettings,
      GetOpenAIOptions,
      GetImagOptions,
      ChangeEmailSettings,
      ChangeOpenAIOptions,
      ChangeImagOptions,
      GetAlipayOptions, ChangeAlipayOptions
  } from '@/service';
  const updateEmailDisabled=ref(false);
  const updateAiDisabled=ref(false);
  const updateImageDisabled=ref(false);
  const updateAlipayDisabled=ref(false);
  const  formModel = ref<ApiGptManagement.Email>({
	host:"",
    port:0,
    useSsl:false,
    senderEmail:"",
    senderName:"",
    senderPassword:"",
})
const  openaiModel = ref<ApiGptManagement.OpenAIOptions>({
    tokenPrice:0,
    newUserBalance:0,
    openAI:{
    keyList:[],
    maxTokens:1500,
    temperature:0.7,
    frequencyPenalty:0,
    presencePenalty:0,
    chatModel:"gpt-3.5-turbo",
    topP:0.7,
    contextCount:2,
    maxQuestions:100,
  },
    azureOpenAI:null,
  });
  const  imageModel = ref<ApiGptManagement.ImagOptions>({
    imagePrice:0,
    imagFileBaseUrl:"",
    sdOptions:[]
  });
  const  alipayModel = ref<ApiGptManagement.AlipayOptions>({
      appId: "",
      alipayPublicKey: "",
      appPrivateKey: "",
      serverUrl: "",
      version: "",
      signType: "",
      encryptKey: "",
      appPublicCert: "",
      alipayPublicCert: "",
      alipayRootCert: "",
  });
  const Options: { label: string; value: string,disabled: boolean}[] = [
    { label: 'ChatGpt', value: 'ChatGpt',disabled:true },
    { label: 'gpt-3.5-turbo', value: 'gpt-3.5-turbo',disabled:false },
    { label: 'gpt-3.5-turbo-16k',value: 'gpt-3.5-turbo-16k',disabled:false },
    { label: 'gpt-4', value: 'gpt-4',disabled:false },
    { label: 'gpt-4-vision-preview', value: 'gpt-4-vision-preview',disabled:false },
    { label: 'ChatGLM', value: 'ChatGLM',disabled:true },
    { label: 'ChatGLM', value: 'ChatGLM',disabled:false },
		{ label: 'chatglm3_turbo', value: 'chatglm3_turbo',disabled:false },
      { label: '文心千帆', value: '文心千帆',disabled:true },
      { label: 'ERNIE_Bot_4', value: 'completions_pro',disabled:false },
      { label: 'ERNIE_Bot_8K', value: 'ernie_bot_8k',disabled:false },
			{ label: 'ERNIE_Bot', value: 'completions',disabled:false },
      { label: 'ERNIE_Bot_turbo', value: 'eb-instant',disabled:false },
      { label: 'ERNIE_Bot_turbo_AI', value: 'ai_apaas',disabled:false },
      { label: 'BLOOMZ_7B', value: 'bloomz_7b1',disabled:false },
      { label: 'Qianfan_BLOOMZ_7B_compressed', value: 'qianfan_bloomz_7b_compressed',disabled:false },
      { label: 'Llama_2_7b_chat', value: 'llama_2_7b',disabled:false },
      { label: 'Llama_2_13b_chat', value: 'llama_2_13b',disabled:false },
      { label: 'Llama_2_70b_chat', value: 'llama_2_70b',disabled:false },
      { label: 'Qianfan_Chinese_Llama_2_7B', value: 'qianfan_chinese_llama_2_7b',disabled:false },
      { label: 'Qianfan_Chinese_Llama_2_13B', value: 'qianfan_chinese_llama_2_13b',disabled:false },
      { label: 'ChatGLM2_6B_32K', value: 'chatglm2_6b_32k',disabled:false },
      { label: 'XuanYuan_70B_Chat_4bit', value: 'xuanyuan_70b_chat',disabled:false },
      { label: 'AquilaChat_7B', value: 'aquilachat_7b',disabled:false },
      { label: 'Stable-Diffusion-XL', value: 'Stable-Diffusion-XL',disabled:false },
      { label: '同义千问', value: '同义千问',disabled:true },
      { label: 'qwen-max', value: 'qwen-max',disabled:false },
      { label: 'qwen-turbo', value: 'qwen-turbo',disabled:false },
      { label: 'qwen-plus', value: 'qwen-plus',disabled:false },
      { label: 'stable diffusion', value: 'stable diffusion',disabled:true },
      { label: '二次元', value: 'SD2D',disabled:false },
      { label: '真人', value: 'SD3D',disabled:false },
  ]
  const ModelOptions: { label: string; value: number,disabled: boolean}[] = [
      { label: 'OpenAI', value: 1,disabled:false },
      { label: '文心千帆', value: 2,disabled:false },
      { label: '通义千问',value: 3,disabled:false },
      { label: 'stable diffusion', value: 4,disabled:false },
      { label: 'Midjornay', value: 5,disabled:false },
  ]
  const removeItem = (index:number) => {
  openaiModel.value.openAI!.keyList.splice(index, 1);
};

const addItem = () => {
  openaiModel.value.openAI!.keyList.push({key:'',baseUrl:'',isEnable:true,modelTypes:[],type:1});
};

  async function GetEmailSetting() {
    const { data } = await GetEmailSettings();
    if(data !=null){
        formModel.value=data;
    }
  }

  async function GetOpenAIOption() {
    const { data } = await GetOpenAIOptions();
    if(data !=null && data.openAI !=null){
      openaiModel.value=data;
    }
  }

  async function GetImageOption() {
    const { data } = await GetImagOptions();
    console.log(data);
    if(data !=null && data.sdOptions !=null){
        imageModel.value=data;
    }
    console.log(imageModel);
  }

  async function GetAlipayOption() {
      const { data } = await GetAlipayOptions();
      if(data !=null){
          alipayModel.value=data;
      }
  }

  async function handleGptChange(value:Boolean) {
    if(!value){
        const { data } = await ChangeOpenAIOptions(openaiModel.value);
        if(data){
            window.$message?.success('保存成功！');
        }
    }
  }
  async function handleEmailChange(value:Boolean) {
    if(!value){
        const { data } = await ChangeEmailSettings(formModel.value);
        if(data){
            window.$message?.success('保存成功！');
        }
    }
  }
  async function handleAlipayChange(value:Boolean) {
      if(!value){
          const { data } = await ChangeAlipayOptions(alipayModel.value);
          if(data){
              window.$message?.success('保存成功！');
          }
      }
  }
  async function handleImageChange(value:Boolean) {
    if(!value){
        const { data } = await ChangeImagOptions(imageModel.value);
        if(data){
            window.$message?.success('保存成功！');
        }
    }
  }

  async function init() {
    await GetEmailSetting();
    await GetOpenAIOption();
    await GetImageOption();
    await GetAlipayOption();
  }

//   init();
  onMounted(() => {
    init();
});
  </script>

  <style scoped></style>
