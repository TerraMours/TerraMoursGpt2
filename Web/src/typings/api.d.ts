// 后端接口返回的数据类型

/** 后端返回的用户权益相关类型 */
declare namespace ApiAuth {
  /** 返回的token和刷新token */
  interface Token {
    token: string;
    refreshToken: string;
  }
  /** 返回的用户信息 */
  type UserInfo = Auth.UserInfo;
}

/** 后端返回的路由相关类型 */
declare namespace ApiRoute {
  /** 后端返回的路由数据类型 */
  interface Route {
    /** 动态路由 */
    routes: AuthRoute.Route[];
    /** 路由首页对应的key */
    home: AuthRoute.AllRouteKey;
  }
}
declare namespace ApiCommon {
  interface KeyValue {
    key: number | string;
    value: string;
  }
}

declare namespace ApiUserManagement {
  interface User {
    /** 用户id */
    userId: number;
    /** 用户名 */
    userName: string | null;
    /** 用户年龄 */
    age: number | null;
    /**
     * 用户性别
     * - 0: 女
     * - 1: 男
     */
    gender: '0' | '1' | null;
    /** 用户手机号码 */
    userPhoneNum: string | null;
    /** 用户邮箱 */
    userEmail: string | null;
    /**
     * 用户状态
     * - 1: 启用
     * - 2: 禁用
     * - 3: 冻结
     * - 4: 软删除
     */
    userStatus: '1' | '2' | '3' | '4' | null;
    /**
     * 是否能登陆
     * - true: 启用
     * - false: 禁用
     */
    enableLogin: boolean;
    /** 角色id */
    roleId: number;
    /** 余额 */
    balance: number;
  }
  interface Role {
    /** 角色id */
    roleId: number;
    /** 角色名称 */
    roleName: string | null;
    /** 创建时间 */
    createDate: Date | null;
    /** 更新时间 */
    modifyDate: Date | null;
    isAdmin: boolean;
    isNewUser: boolean;
  }
  interface Menu {
    /** 菜单id */
    menuId: number;
    /** 菜单父级id */
    parentId: number | null;
    /** 菜单名称 */
    menuName: string | null;
    /** 菜单url地址 */
    menuUrl: string | null;
    /** 菜单图标 */
    icon: string | null;
    /** 菜单描述or说明 */
    description: string | null;
    /** 是否是首页 */
    isHome: boolean;
    /** 是否是外链链接，默认是false，默认是内部系统地址 */
    externalUrl: boolean;
    /** 是否可见，默认true */
    isShow: boolean;
    /** 备注 */
    remark: string | null;
    /** 排序 */
    orderNo: number;
    /** 子节点 */
    children: Menu[] | null;
    /** 是否勾选 */
    isChecked: boolean | null;
  }
}

declare namespace ApiGptManagement {
  interface PageData<T> {
    items: T[];
    total: number;
    page: number;
    pageSize: number;
  }
  //* *敏感词 */
  interface Sensitive {
    sensitiveId: number;
    word: string;
  }
  /** 聊天记录 */
  interface Chat {
    chatRecordId: number;
    conversationId: number;
    role: string;
    message: string;
    userId: number;
    userName: string;
    createDate: Date;
    promptTokens: number;
    completionTokens: number;
    totalTokens: number;
    error?: boolean;
    loading?: boolean;
    model: string;
    modelType: string;
  }
  interface Conversations {
    conversationId: number;
    conversationName: string;
    KnowledgeId: number;
    knowledgeName: string;
    imagePath: string;
    isEdit: boolean;
  }
  /** 图片记录 */
  interface Image {
    collectCount: number;
    createDate: Date;
    forwardCount: number;
    imagUrl: string;
    imageRecordId: number;
    ip: string;
    isPublic: boolean;
    likeCount: number;
    model: string;
    modelType?: number;
    pranslatePrompt?: string;
    prompt?: string;
    size?: number;
    userId: number;
    userName: string;
  }
  /** key池 管理 */
  interface KeyOption {
    keyId: number;
    apiKey: string;
    expirationTime: Date | null;
    used: number | null;
    unUsed: number | null;
    total: number | null;
    createDate: Date | null;
  }
  /** 系统提示词 */
  interface PromptOption {
    promptId: number;
    act: string | null;
    prompt: string | null;
    usedCount: number | null;
    createDate: Date | null;
  }
  /** 邮箱设置 */
  interface Email {
    host: string;
    port: number;
    useSsl: boolean;
    senderEmail: string;
    senderName: string;
    senderPassword: string;
  }
  /** AI配置 */
  interface OpenAIOptions {
    tokenPrice: number;
    newUserBalance: number;
    openAI: OpenAI | null;
    azureOpenAI: null;
  }
  /** openai */
  interface OpenAI {
    keyList: AKeyOption[];
    maxTokens: number | null;
    temperature: number | null;
    frequencyPenalty: number | null;
    presencePenalty: number | null;
    chatModel: string;
    topP: number | null;
    contextCount: number | null;
    maxQuestions: number | null;
  }
  interface AKeyOption {
    key: string;
    baseUrl: string;
    isEnable: boolean;
    modelTypes: string[]; // 将属性类型改为字符串数组
    type: number;
  }

  /** 图片生成配置 */
  interface ImagOptions {
    imagePrice: number;
    imagFileBaseUrl: string | null;
    sdOptions: SDOptions[];
  }
  /** Stable Diffusion 配置 */
  interface SDOptions {
    label: string | null;
    baseUrl: string | null;
    negative_Prompt: string | null;
  }

  /**
   * 支付配置
   */
  interface AlipayOptions {
    appId: string | null;
    alipayPublicKey: string | null;
    appPrivateKey: string | null;
    serverUrl: string | null;
    version: string | null;
    signType: string | null;
    encryptKey: string | null;
    appPublicCert: string | null;
    alipayPublicCert: string | null;
    alipayRootCert: string | null;
  }
  interface ImageRes {
    imageRecordId: number;
    prompt: string | null;
    pranslatePrompt: string | null;
    imagUrl: string;
    forwardCount: number;
    collectCount: number;
    likeCount: number;
    createDate: Date;
    isPublic: boolean | null;
    isForward: boolean | null;
    isCollect: boolean | null;
    islike: boolean | null;
  }
  interface SubmitDTO {
    negativePrompt: string;
    Prompt: string;
    model: string;
    connectionId: any;
    Count: number;
    Size: number;
  }
}
/** 支付模块 */
declare namespace ApiPayManagement {
  interface PageData<T> {
    items: T[];
    total: number;
    page: number;
    pageSize: number;
  }
  /** 商品 */
  interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    discount: number;
    categoryId: number;
    categoryName: string;
    stock: number | null;
    isVIP: boolean;
    vipLevel: number | null;
    vipTime: number | null;
    imagePath: string | null | undefined;
  }

  /** 分类 */
  interface Category {
    id: number;
    name: string;
    description: string;
  }

  /**
   * 订单
   */
  interface Order {
    id: number;
    orderId: string;
    tradeNo: string;
    productId: 0;
    name: string;
    description: string;
    price: number;
    stock: number;
    imagePath: string;
    userId: string;
    userName: string;
    status: string;
    createdTime: Date;
    paidTime: Date;
    isVIP: boolean;
    vipLevel: number;
    vipTime: number;
  }
  interface AlipayResponse {
    out_trade_no: string;
    qr_code: string;
  }
}
/**
 * 统计模块
 */
declare namespace ApiAnalysisManagement {
  /**
   * 数量统计
   */
  interface TotalAnalysis {
    key: string;
    total: number;
    lastTotal: number;
  }
  interface AllAnalysis {
    key: string;
    askCount: number;
    userCount: number;
    imageCount: number;
  }
}

declare namespace ApiKnowledgeManagement {
  interface Knowledge {
    knowledgeId: number;
    knowledgeName: string;
    isCommon: boolean;
    knowledgeType: number;
    apiKey: string;
    indexName: string;
    workSpace: string;
    nameSpace: string;
    baseUrl: string;
    imagePath: string | null | undefined;
    remark: string | null | undefined;
  }
  interface Vector {
    matches: ScoredVector[];
    namespace: string;
  }

  interface ScoredVector {
    id: string;
    score: number;
    values: any;
    metadata: any;
  }
  interface IndexStats {
    namespaces: IndexNamespace[];
    totalVectorCount: number;
    indexFullness: number;
    dimension: number;
  }
  interface IndexNamespace {
    name: string;
    vectorCount: number;
  }
}
