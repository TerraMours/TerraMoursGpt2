import { defineStore } from 'pinia';
import { localStg } from '@/utils';

function defaultState(): Completion.ChatState {
  return {
    activeId: 0,
    siderCollapsed: false
  };
}

function getLocalState(): Completion.ChatState {
  const chatStorage:Completion.ChatState=localStg.get('chatStorage') || defaultState();
  return chatStorage;
}

function setLocalState(state: Completion.ChatState) {
  localStg.set('chatStorage', state);
}
export const useChatState = defineStore('chat-store', {
  state: (): Completion.ChatState => getLocalState(),
  getters: {
    /** 是否登录 */
    active(state) {
      return state.activeId;
    }
  },
  actions: {
    async setActive(uuid: number) {
      this.activeId = uuid;
      setLocalState(this.$state);
    },
    setSiderCollapsed(collapsed: boolean) {
      this.siderCollapsed = collapsed;
      setLocalState(this.$state);
    }
  }
});
