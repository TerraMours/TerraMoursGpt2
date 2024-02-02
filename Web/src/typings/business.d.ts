/** 用户相关模块 */
declare namespace Auth {
  /**
   * 用户角色类型(前端静态路由用角色类型进行路由权限的控制)
   * - super: 超级管理员(该权限具有所有路由数据)
   * - admin: 管理员
   * - user: 用户
   */
  type RoleType = 'super' | 'admin' | 'user';

  /** 用户信息 */
  interface UserInfo {
    /** 用户id */
    userId: number;
    /** 用户名 */
    userName: string;
    /** 用户角色类型 */
    roleId: RoleType;
    headImageUrl: string | null | undefined; // 头像url
    vipLevel?: number; // vip等级
    vipExpireTime?: Date; // vip过期时间
    imageCount?: string; // 剩余图片使用次数
    balance: number; // 用户余额
  }
}

declare namespace UserManagement {
  interface User extends ApiUserManagement.User {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }

  interface Role extends ApiUserManagement.Role {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }

  interface Menu extends ApiUserManagement.Menu {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  /**
   * 用户性别
   * - 0: 女
   * - 1: 男
   */
  type GenderKey = NonNullable<User['gender']>;

  /**
   * 用户状态
   * - 1: 启用
   * - 2: 禁用
   * - 3: 冻结
   * - 4: 软删除
   */
  type UserStatusKey = NonNullable<User['userStatus']>;

  /**
   * 是否能登陆
   * - true: 启用
   * - false: 禁用
   */
  type EnableLoginKey = NonNullable<User['enableLogin']>;
}

declare namespace GptManagement {
  interface Sensitive extends ApiGptManagement.Sensitive {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }

  interface Chat extends ApiGptManagement.Chat {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  interface Image extends ApiGptManagement.Image {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  interface KeyOption extends ApiGptManagement.KeyOption {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  interface PromptOption extends ApiGptManagement.PromptOption {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
}

/** 支付模块 */
declare namespace PayManagement {
  /** 商品 */
  interface Product extends ApiPayManagement.Product {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  /** 分类 */
  interface Category extends ApiPayManagement.Category {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  interface Order extends ApiPayManagement.Order {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
}
declare namespace Completion {
  interface ChatState {
    /** 当前会话id */
    activeId: number;
    siderCollapsed: boolean;
  }
}
/** 知识库模块 */
declare namespace KnowledgeManagement {
  interface Knowledge extends ApiKnowledgeManagement.Knowledge {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
  }
  interface ScoredVector extends ApiKnowledgeManagement.ScoredVector {
    /** 序号 */
    index: number;
    /** 表格的key（id） */
    key: number;
    namespace: string;
    knowledgeId: number;
  }
}
