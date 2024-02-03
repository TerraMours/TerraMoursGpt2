import { mockRequest,request,baseUrl } from '../request';

export const apiUrl=baseUrl;
/**
 * 获取验证码
 * @param phone - 手机号
 * @returns - 返回boolean值表示是否发送成功
 */
export function fetchSmsCode(phone: string) {
  return mockRequest.post<boolean>('/getSmsCode', { phone });
  // return mockRequest.post<boolean>('/getSmsCode', { phone });
}

/**
 * 获取邮箱验证码
 * @param userEmail - 邮箱
 * @returns - 返回boolean值表示是否发送成功
 */
export function fetchEmailCode(userEmail: string) {
  return request.post<boolean>('/api/v1/Email/CreateCheckCode', { userEmail });
}

/**
 * 登录(登录接口)
 * @param userAccount - 用户名
 * @param userPassword - 密码
 */
export function fetchLogin(userAccount: string, userPassword: string) {
  return request.post<ApiAuth.Token>('/api/v1/Login/Login', { userAccount, userPassword });
  // return mockRequest.post<ApiAuth.Token>('/login', { userName, password });
}

/** 获取用户信息 */
export function fetchUserInfo() {
  return request.get<ApiAuth.UserInfo>('/api/v1/User/GetUser');
}

/**
 * 获取用户路由数据
 * @param userId - 用户id
 * @description 后端根据用户id查询到对应的角色类型，并将路由筛选出对应角色的路由数据返回前端
 */
export function fetchUserRoutes(userId: number) {
  return request.post<ApiRoute.Route>('/api/v1/Menu/GetUserRoutes', { userId });
}

/**
 * 刷新token
 * @param refreshToken
 */
export function fetchUpdateToken(refreshToken: string) {
  return mockRequest.post<ApiAuth.Token>('/updateToken', { refreshToken });
}

/**
 * 注册接口
 * @param userAccount - 登录账号
 * @param userPassword - 密码
 * @param repeatPassword - 密码确认
 * @param checkCode - 校验码
 * @returns
 */
export function fetchRegister(userAccount: string,userPassword: string,repeatPassword: string,checkCode: string) {
  return request.post<string>('/api/v1/Login/Register', { userAccount, userPassword,repeatPassword,checkCode});
}

/**
 * 修改密码接口
 * @param userAccount - 登录账号
 * @param userPassword - 密码
 * @param repeatPassword - 密码确认
 * @param checkCode - 校验码
 * @returns
 */
export function fetchChangePassword(userAccount: string,userPassword: string,repeatPassword: string,checkCode: string) {
	return request.post<string>('/api/v1/Login/ChangePassword', { userAccount, userPassword,repeatPassword,checkCode});
}

/**
 * 删除用户
 * @param userId
 * @returns
 */
export function fetchDelUser(userId: number){
  return request.post<boolean>('api/v1/User/DelUser',{userId});
}
/**
 * 更新用户信息
 * @param userId
 * @param userName
 * @param userEmail
 * @param userPhoneNum
 * @param gender
 * @param enableLogin
 * @returns
 */
export function fetchUpdateUser(userId:number  | null,userName:string | null,headImageUrl: string | null | undefined,userEmail:string | null,userPhoneNum:string | null,gender:string | null,enableLogin:boolean | null,roleId:number |null,balance:number|null){
return request.post<boolean>('/api/v1/User/UpdateUser',{userId,userName,userEmail,userPhoneNum,gender,enableLogin,roleId,balance,headImageUrl});
}

/**
 * 新增用户信息
 * @param userId
 * @param userName
 * @param userEmail
 * @param userPhoneNum
 * @param gender
 * @param enableLogin
 * @returns
 */
export function fetchAddUser(userName:string | null,userEmail:string | null,userPhoneNum:string | null,gender:string | null,enableLogin:boolean | null,roleId:number |null){
  return request.post<boolean>('/api/v1/User/AddUser',{userName,userEmail,userPhoneNum,gender,enableLogin,roleId});
  }

  /**
 * 删除角色
 * @param roleId
 * @returns
 */
export function fetchDelRole(roleId: number){
  return request.post<boolean>('api/v1/Role/DelRole',{roleId});
}
/**
 * 更新角色信息
 * @param roleId
 * @param roleName
 * @returns
 */
export function fetchUpdateRole(roleId:number  | null,roleName:string | null,isAdmin:boolean,isNewUser:boolean){
return request.post<boolean>('/api/v1/Role/UpdateRole',{roleId,roleName,isAdmin,isNewUser});
}

/**
 * 新增角色信息
 * @param roleName
 * @returns
 */
export function fetchAddRole(roleName:string | null,isAdmin:boolean,isNewUser:boolean){
  return request.post<boolean>('/api/v1/Role/AddRole',{roleName,isAdmin,isNewUser});
}


  /**
 * 删除菜单
 * @param menuId
 * @returns
 */
  export function fetchDelMenu(menuId: number){
    return request.post<boolean>('api/v1/Menu/DelMenu',{menuId});
  }
  /**
   * 更新菜单信息
   * @param roleId
   * @param roleName
   * @returns
   */
  export function fetchUpdateMenu(menuId:number | null,parentId:number | null,menuName:string |null,menuUrl: string|null,icon: string|null,description: string|null,isHome:boolean,externalUrl:boolean,isShow:boolean,remark:string|null,orderNo:number|null){
  return request.post<boolean>('/api/v1/Menu/UpdateMenu',{menuId,parentId,menuName,menuUrl,icon,description,isHome,externalUrl,isShow,remark,orderNo});
  }

  /**
   * 新增菜单信息
   * @param roleName
   * @returns
   */
  export function fetchAddMenu(parentId:number | null,menuName:string |null,menuUrl: string|null,icon: string|null,description: string|null,isHome:boolean,externalUrl:boolean,isShow:boolean,remark:string|null,orderNo:number|null){
    return request.post<boolean>('/api/v1/Menu/AddMenu',{parentId,menuName,menuUrl,icon,description,isHome,externalUrl,isShow,remark,orderNo});
  }
  /**
 * 角色匹配菜单
 * @param menuIds
 * @returns
 */
   export function fetchAddMenuToRole(roleId: number | null,menuIds: number[]){
    return request.post<boolean>('api/v1/Menu/AddMenuToRole',{roleId,menuIds});
  }

   /**
 * 角色下的菜单id
 * @param roleId
 * @returns
 */
   export function fetchGetRoleMenusIds(roleId: number | null){
    return request.post<number[]>('api/v1/Menu/GetRoleMenusIds',{roleId});
  }
 /**
  * 菜单下拉框
  * @param roleId 角色
  * @param menuId 当前菜单id（排除自己）
  * @returns
  */
  export function fetchGetMenuSelect(roleId: number | null,menuId: number | null){
    return request.post<ApiCommon.KeyValue[]>('api/v1/Menu/GetMenuSelect',{roleId,menuId});
  }

 /**
  * 用户下拉框
  * @param roleId 角色
  * @param menuId 当前菜单id（排除自己）
  * @returns
  */
 export function fetchGetRoleSelect(){
  return request.get<ApiCommon.KeyValue[]>('api/v1/Role/GetRoleSelect');
}

/**
 * 新增敏感词
 * @param word
 * @returns
 */
export function fetchAddSensitive(word: string | null){
  return request.get<boolean>('/api/v1/Chat/AddSensitive',{word});
}
/**
 * 修改敏感词
 * @param sensitiveId
 * @param word
 * @returns
 */
export function fetchChangeSensitive(sensitiveId:number,word: string | null){
  return request.get<boolean>('/api/v1/Chat/ChangeSensitive',{sensitiveId,word});
}
/**
 * 删除敏感词
 * @param sensitiveId
 * @returns
 */
export function fetchDeleteSensitive(sensitiveId:number){
  return request.get<boolean>('/api/v1/Chat/DeleteSensitive',{sensitiveId});
}

/**
 * 新增key池
 * @param apiKey
 * @returns
 */
export function fetchAddKeyOptions(apiKey: string | null){
  return request.get<boolean>('/api/v1/Chat/AddKeyOptions',{apiKey});
}
/**
 * 修改key池
 * @param keyId
 * @param apiKey
 * @returns
 */
export function fetchChangeKeyOptions(keyId:number,apiKey: string | null){
  return request.get<boolean>('/api/v1/Chat/ChangeKeyOptions',{keyId,apiKey});
}
/**
 * 删除key池
 * @param keyId
 * @returns
 */
export function fetchDeleteKeyOptions(keyId:number){
  return request.get<boolean>('/api/v1/Chat/DeleteKeyOptions',{keyId});
}

/**
 * 新增系统提示词
 * @param act 扮演角色
 * @param Prompt 提示词
 * @returns
 */
export function fetchAddPromptOption(act: string | null,Prompt: string | null){
  return request.post<boolean>('/api/v1/Chat/AddPromptOption',{act,Prompt});
}
/**
 * 修改系统提示词
 * @param promptId
 * @param act 扮演角色
 * @param Prompt 提示词
 * @returns
 */
export function fetchChangePromptOption(promptId:number,act: string | null,Prompt: string | null){
  return request.post<boolean>('/api/v1/Chat/ChangePromptOption',{promptId,act,Prompt});
}
/**
 * 删除系统提示词
 * @param promptId
 * @returns
 */
export function fetchDeletePromptOption(promptId:number){
  return request.get<boolean>('/api/v1/Chat/DeletePromptOption',{promptId});
}

/**
 * 新增商品分类
 * @param Name 分类名称
 * @param Description 分类描述
 * @returns
 */
export function AddCategory(Name: string,Description: string){
  return request.post<boolean>('/api/v1/Category/AddCategory',{Name,Description});
}
/**
 * 更新分类信息
 * @param Id
 * @param Name 分类名称
 * @param Description 分类描述
 * @returns
 */
export function UpdateCategory(Id:number,Name: string,Description: string){
  return request.put<boolean>('/api/v1/Category/UpdateCategory',{Id,Name,Description});
}
/**
 * 删除分类信息
 * @param id
 * @returns
 */
export function DeleteCategory(id:number){
  return request.put<boolean>('/api/v1/Category/DeleteCategory?id='+id);
}

/**
 * 增加商品
 * @param Name 商品名称
 * @param Description 商品描述
 * @param Price 商品价格
 * @param Discount 商品折扣 默认不打折
 * @param CategoryId 商品分类Id
 * @param Stock 商品库存
 * @returns
 */
export function AddProduct(Name: string,Description: string,Price: number,Discount:number,CategoryId:number,Stock:number|null,isVIP: boolean,
  vipLevel: number | null,vipTime: number | null,imagePath:string | null | undefined){
  return request.post<boolean>('/api/v1/Product/AddProduct',{Name,Description,Price,Discount,CategoryId,Stock,isVIP,vipLevel,vipTime,imagePath});
}
/**
 * 修改商品信息
 * @param Id
 * @param Name 商品名称
 * @param Description 商品描述
 * @param Price 商品价格
 * @param Discount 商品折扣 默认不打折
 * @param CategoryId 商品分类Id
 * @param Stock 商品库存
 * @returns
 */
export function UpdateProduct(Id:number,Name: string,Description: string,Price: number,Discount:number,CategoryId:number,Stock:number|null,isVIP: boolean,
  vipLevel: number | null,vipTime: number | null,imagePath:string | null | undefined){
  return request.put<boolean>('/api/v1/Product/UpdateProduct',{Id,Name,Description,Price,Discount,CategoryId,Stock,isVIP,vipLevel,vipTime,imagePath});
}
/**
 * 删除商品
 * @param id
 * @returns
 */
export function DeleteProduct(id:number){
  return request.put<boolean>('/api/v1/Product/DeleteProduct?id='+id);
}
/** 商品购买 */
export function PreCreate(Name: string, Price: number, Description: string, ProductId: number, isvip: boolean | null, vipLevel: number | null, vipTime: number | null) {
  return request.post<ApiPayManagement.AlipayResponse>(
      '/api/v1/AliPay/PreCreate',
      {Name, Price, Description, isvip, vipLevel, vipTime, ProductId}
  )
}

/**获取邮箱设置 */
export function GetEmailSettings(){
  return request.get<ApiGptManagement.Email>('/api/v1/Settings/GetEmailSettings');
}
/**修改邮箱设置 */
export function ChangeEmailSettings(email:ApiGptManagement.Email){
  return request.post<boolean>('/api/v1/Settings/ChangeEmailSettings',email);
}
/**获取支付设置 */
export function GetAlipayOptions(){
  return request.get<ApiGptManagement.AlipayOptions>('/api/v1/Settings/GetAlipayOptions');
}
/**修改支付设置 */
export function ChangeAlipayOptions(alipay:ApiGptManagement.AlipayOptions){
  return request.post<boolean>('/api/v1/Settings/ChangeAlipayOptions',alipay);
}

/**获取AI设置 */
export function GetOpenAIOptions(){
  return request.get<ApiGptManagement.OpenAIOptions>('/api/v1/Settings/GetOpenAIOptions');
}
/**修改AI设置 */
export function ChangeOpenAIOptions(email:ApiGptManagement.OpenAIOptions){
  return request.post<boolean>('/api/v1/Settings/ChangeOpenAIOptions',email);
}
/**获取Key池 */
export function GetKeyList(){
	return request.get<ApiGptManagement.AKeyOption[]>('/api/v1/Settings/GetKeyList');
}
/**修改Key池 */
export function ChangKeyList(keys:ApiGptManagement.AKeyOption[]){
	return request.post<boolean>('/api/v1/Settings/ChangKeyList',keys);
}
/**获取图片生成配置 */
export function GetImagOptions(){
  return request.get<ApiGptManagement.ImagOptions>('/api/v1/Settings/GetImagOptions');
}
/**修改图片生成设置 */
export function ChangeImagOptions(email:ApiGptManagement.ImagOptions){
  return request.post<boolean>('/api/v1/Settings/ChangeImagOptions',email);
}

/**导入系统提示词(文件) */
export function ImportPromptOptionByFile(file:File){
  return request.post<boolean>('/api/v1/Chat/ImportPromptOptionByFile',{file},{headers : {"Content-Type":'multipart/form-data'}});
}

/**导入敏感词字典(文件) */
export function ImportSensitive(file:File){
  return request.post<boolean>('/api/v1/Chat/ImportSensitive',{file},{headers : {"Content-Type":'multipart/form-data'}});
}

/**分享图片 */
export function ShareImage(imageRecordId: number, isPublic: boolean){
	return request.get<boolean>('/api/v1/Image/ShareImage',{imageRecordId,isPublic});
}
/** 新增知识库 */
export function fetchAddKnowledge(knowledgeName:string | null,apiKey:string | null,indexName:string | null,namespace:string | null,workSpace:string | null,baseUrl:string | null,isCommon:boolean | null,knowledgeType:number |null,imagePath:string | null | undefined,remark: string | null | undefined){
  return request.post<boolean>('/api/v1/Knowledge/Upsert',{knowledgeName,apiKey,indexName,namespace,workSpace,baseUrl,isCommon,knowledgeType,imagePath,remark});
}
/** 更新知识库 */
export function fetchKnowledgeDescribeIndex(knowledgeName:string | null,apiKey:string | null,indexName:string | null,namespace:string | null,workSpace:string | null,baseUrl:string | null,isCommon:boolean | null,knowledgeType:number |null){
  return request.post<ApiKnowledgeManagement.IndexStats>('/api/v1/Knowledge/DescribeIndexStats',{knowledgeName,apiKey,indexName,namespace,workSpace,baseUrl,isCommon,knowledgeType});
}
/** 验证知识库 */
export function fetchUpdateKnowledge(KnowledgeId:number,knowledgeName:string | null,apiKey:string | null,indexName:string | null,namespace:string | null,workSpace:string | null,baseUrl:string | null,isCommon:boolean | null,knowledgeType:number |null,imagePath:string | null | undefined,remark: string | null | undefined){
  return request.post<boolean>('/api/v1/Knowledge/Update',{KnowledgeId,knowledgeName,apiKey,indexName,namespace,workSpace,baseUrl,isCommon,knowledgeType,imagePath,remark});
}
/** 删除指定知识库 */
export function fetchDeleteKnowledge (id: number) {
  return request.get<boolean>('/api/v1/Knowledge/Delete', { id });
}

/** 查询指定知识库 */
export function fetchQueryKnowledge (id: number) {
  return request.get<ApiKnowledgeManagement.Knowledge>('/api/v1/Knowledge/Query', { id });
}

/**
 * 修改vector
 * @param word
 * @returns
 */
export function fetchUpdateVector(knowledgeId: number | null,
                                  id: string | null,
                                  values: number[] | null,
                                  setMetadata: [] | null,
                                  namespace: string | null){
  return request.post<boolean>('/'+knowledgeId+'/api/v1/Vector/Update',{id,values,setMetadata,namespace});
}
/**
 * 新增vector
 * @param sensitiveId
 * @param word
 * @returns
 */
export function fetchUpsertVector(knowledgeId: number | null,vectors:ApiKnowledgeManagement.ScoredVector[],namespace: string | null){
  return request.post<boolean>('/'+knowledgeId+'/api/v1/Vector/Upsert',{vectors,namespace});
}

export function fetchEmbaddingUpsert(knowledgeId: number | null,vectors:ApiKnowledgeManagement.ScoredVector[],namespace: string | null){
  return request.post<boolean>('/'+knowledgeId+'/api/v1/Vector/EmbaddingUpsert',{vectors,namespace});
}

export function fetchDescribeIndexStats (knowledgeId: number) {
  return request.get<ApiKnowledgeManagement.IndexStats>('/'+knowledgeId+'/api/v1/Vector/DescribeIndexStats');
}

export function GenerateGraph(submitDTO: ApiGptManagement.SubmitDTO) {
  return request.post<string>(
    '/api/v1/Image/GenerateGraph',
    submitDTO
  )
}

