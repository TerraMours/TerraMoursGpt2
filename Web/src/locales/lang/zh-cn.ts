import type { LocaleMessages } from 'vue-i18n';

const locale: LocaleMessages<I18nType.Schema> = {
  message: {
    system: {
      title: 'TerraMours管理系统'
    },
    routes: {
      dashboard: {
        dashboard: '仪表盘',
        analysis: '分析页',
        workbench: '工作台'
      },
      about: {
        about: '关于'
      }
    },
    chat: {
      newChatButton: '新建聊天',
      placeholder: '来说点什么吧...（Shift + Enter = 换行，"/" 触发提示词）',
      placeholderMobile: '来说点什么...',
      copy: '复制',
      copied: '复制成功',
      copyCode: '复制代码',
      clearChat: '清空会话',
      clearChatConfirm: '是否清空会话?',
      exportImage: '保存会话到图片',
      exportImageConfirm: '是否将会话保存为图片?',
      exportSuccess: '保存成功',
      exportFailed: '保存失败',
      usingContext: '上下文模式',
      turnOnContext: '当前模式下, 发送消息会携带之前的聊天记录',
      turnOffContext: '当前模式下, 发送消息不会携带之前的聊天记录',
      deleteMessage: '删除消息',
      deleteMessageConfirm: '是否删除此消息?',
      deleteHistoryConfirm: '确定删除此记录?',
      clearHistoryConfirm: '确定清空聊天记录?',
      preview: '预览',
      showRawText: '显示原文',
      delete: '删除'
    },
    setting: {
      avatarLink: '头像链接',
      name: '名称',
      description: '描述',
      vipLevel: '会员等级',
      vipExpireTime: '会员到期时间',
      tobeVip: '充值',
      save: '保存',
      balance: '用户余额'
    }
  }
};

export default locale;
