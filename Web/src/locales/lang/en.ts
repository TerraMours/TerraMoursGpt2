import type { LocaleMessages } from 'vue-i18n';

const locale: LocaleMessages<I18nType.Schema> = {
  message: {
    system: {
      title: 'TerraMoursAdmin'
    },
    routes: {
      dashboard: {
        dashboard: 'Dashboard',
        analysis: 'Analysis',
        workbench: 'Workbench'
      },
      about: {
        about: 'About'
      }
    },
    chat: {
      newChatButton: 'New Chat',
      placeholder: 'Ask me anything...(Shift + Enter = line break, "/" to trigger prompts)',
      placeholderMobile: 'Ask me anything...',
      copy: 'Copy',
      copied: 'Copied',
      copyCode: 'Copy Code',
      clearChat: 'Clear Chat',
      clearChatConfirm: 'Are you sure to clear this chat?',
      exportImage: 'Export Image',
      exportImageConfirm: 'Are you sure to export this chat to png?',
      exportSuccess: 'Export Success',
      exportFailed: 'Export Failed',
      usingContext: 'Context Mode',
      turnOnContext: 'In the current mode, sending messages will carry previous chat records.',
      turnOffContext: 'In the current mode, sending messages will not carry previous chat records.',
      deleteMessage: 'Delete Message',
      deleteMessageConfirm: 'Are you sure to delete this message?',
      deleteHistoryConfirm: 'Are you sure to clear this history?',
      clearHistoryConfirm: 'Are you sure to clear chat history?',
      preview: 'Preview',
      showRawText: 'Show as raw text',
      delete: 'Delete'
    },
    setting: {
      avatarLink: 'Avatar Link',
      name: 'Name',
      description: 'Description',
      vipLevel: 'VipLevel',
      vipExpireTime: 'VipExpireTime',
      tobeVip: 'TobeVip',
      save: 'Save',
      balance: 'Balance'
    }
  }
};

export default locale;
