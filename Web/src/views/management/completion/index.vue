<template>
	<div class="flex flex-col w-full h-full">
<n-layout class="z-40 transition h-full" has-sider >
	<List ref="ListComp" @refreshChat="refreshChat"/>
	<n-layout-content>
		<Chat ref="ChatComp" @addConversation="addChatConversation"/>
	</n-layout-content>
</n-layout>
	</div>
</template>
<script setup lang="tsx">
import { List, Chat } from './components';
import {ref} from "vue";
// List组件实例
const ListComp = ref<null | any>(null) // 将类型设置为允许为 null
const ChatComp = ref<null | any>(null) // 将类型设置为允许为 null
async function addChatConversation(data:ApiGptManagement.Conversations) {
  ListComp!.value.addFaultConversation(data);
}
async function refreshChat(){
  ChatComp!.value.refreshVue();
}
</script>
