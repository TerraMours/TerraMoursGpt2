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
