import type { AxiosProgressEvent, GenericAbortSignal } from 'axios';
import { adapter } from '@/utils';
import { request } from '../request';
import {
  adapterOfFetchUserList,
  adapterOfFetchRoleList,
  adapterOfFetchMenuList,
  adapterOfFetchMenuTree,
  adapterOfFetchPageSensitiveList,
  adapterOfFetchPageChatList,
  adapterOfFetchPageKeyOptionList,
  adapterOfFetchPagePromptOptionList,
  adapterAllCategoryList,
  adapterAllProductList,
  adapterOfFetchPageAllImageList,
  adapterOfFetchPageOrderList,
  adapterOfFetchKnowledgeList,
  adapterOfFetchVectorList
} from './management.adapter';

/** 获取用户列表 */
export const fetchUserList = async () => {
  const data = await request.get<ApiUserManagement.User[] | null>('/api/v1/User/GetAllUserList');
  return adapter(adapterOfFetchUserList, data);
};

/** 获取角色列表 */
export const fetchRoleList = async () => {
  const data = await request.get<ApiUserManagement.Role[] | null>('/api/v1/Role/GetAllRoleList');
  return adapter(adapterOfFetchRoleList, data);
};

/** 获取菜单列表 */
export const fetchMenuList = async () => {
  const data = await request.get<ApiUserManagement.Menu[] | null>('/api/v1/Menu/GetAllMenuList');
  return adapter(adapterOfFetchMenuList, data);
};

/** 获取菜单列表 */
export const fetchMenuTreeList = async (roleId: number | null) => {
  const data = await request.post<ApiUserManagement.Menu[] | null>('/api/v1/Menu/GetMenuTree', { roleId });
  return adapter(adapterOfFetchMenuTree, data);
};

/** 获取敏感词列表 */
export const fetchSensitiveList = async (
  queryString: string | null,
  pageIndex: number | undefined,
  pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.Sensitive> | null>(
    '/api/v1/Chat/SensitiveList',
    {
      queryString,
      pageIndex,
      pageSize
    }
  );
  return adapter(adapterOfFetchPageSensitiveList, data);
};

/** 获取聊天记录列表 */
export const fetchChatList = async (
  conversationId: number | null,
  queryString: string | null,
  pageIndex: number | undefined,
  pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.Chat> | null>(
    '/api/v1/Chat/ChatRecordList',
    { conversationId, queryString, pageIndex, pageSize }
  );
  return adapter(adapterOfFetchPageChatList, data);
};
/** 删除会话 */
export const fetchDeleteChatRecord = async (recordId: number) => {
  return request.get<boolean>('/api/v1/Chat/DeleteChatRecord', { recordId });
};
/** 获取聊天记录列表 */
export const fetchAllImageList = async (
  queryString: string | null,
  pageIndex: number | undefined,
  pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.Image> | null>(
    '/api/v1/Image/AllImageList',
    { queryString, pageIndex, pageSize }
  );
  return adapter(adapterOfFetchPageAllImageList, data);
};

/** 获取key池 管理列表 */
export const fetchKeyOptionList = async (
  queryString: string | null,
  pageIndex: number | undefined,
  pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.KeyOption> | null>(
    '/api/v1/Chat/KeyOptionsList',
    {
      queryString,
      pageIndex,
      pageSize
    }
  );
  return adapter(adapterOfFetchPageKeyOptionList, data);
};

/** 获取系统提示词列表 */
export const fetchPromptOptionList = async (
  queryString: string | null,
  pageIndex: number | undefined,
  pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.PromptOption> | null>(
    '/api/v1/Chat/PromptOptionList',
    {
      queryString,
      pageIndex,
      pageSize
    }
  );
  return adapter(adapterOfFetchPagePromptOptionList, data);
};

/** 查询所有商品分类信息 */
export const fetchAllCategoryList = async () => {
  const data = await request.get<ApiPayManagement.Category[] | null>('/api/v1/Category/GetAllCategoryList');
  return adapter(adapterAllCategoryList, data);
};

/** 查询所有商品信息 */
export const fetchAllProductList = async () => {
  const data = await request.get<ApiPayManagement.Product[] | null>('/api/v1/Product/GetAllProductList');
  return adapter(adapterAllProductList, data);
};

/** 获取聊天记录列表 */
export const fetchOrderList = async (
  queryString: string | null,
  pageIndex: number | undefined,
  pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiPayManagement.Order> | null>(
    '/api/v1/AliPay/OrderList',
    { queryString, pageIndex, pageSize }
  );
  return adapter(adapterOfFetchPageOrderList, data);
};

/** 数量统计 */
export const fetchTotalAnalysis = async (dateType: number | null, startTime: string | null, endTime: string | null) => {
  const data = await request.post<ApiAnalysisManagement.TotalAnalysis[] | null>('/api/v1/Analysis/TotalAnalysis', {
    dateType,
    startTime,
    endTime
  });
  return data;
};

/** 数量统计 */
export const fetchAnalysisList = async (
  dateType: number | null,
  startTime: string | null,
  endTime: string | null,
  analysisType: number | null
) => {
  const data = await request.post<ApiAnalysisManagement.TotalAnalysis[] | null>('/api/v1/Analysis/AnalysisList', {
    analysisType,
    dateType,
    startTime,
    endTime
  });
  return data;
};

/** 饼状图数量统计 */
export const fetchPieAnalysisList = async (
  dateType: number | null,
  startTime: string | null,
  endTime: string | null,
  analysisType: number | null
) => {
  const data = await request.post<ApiAnalysisManagement.TotalAnalysis[] | null>('/api/v1/Analysis/PieAnalysisList', {
    analysisType,
    dateType,
    startTime,
    endTime
  });
  return data;
};

/** 销售统计 */
export const fetchSaleAnalysis = async (
  dateType: number | null,
  startTime: string | null,
  endTime: string | null,
  analysisType: number | null
) => {
  const data = await request.post<ApiAnalysisManagement.TotalAnalysis[] | null>('/api/v1/Analysis/SaleAnalysis', {
    analysisType,
    dateType,
    startTime,
    endTime
  });
  return data;
};

/** 所有统计数量 */
export const fetchAllAnalysisList = async (
  dateType: number | null,
  startTime: string | null,
  endTime: string | null
) => {
  const data = await request.post<ApiAnalysisManagement.AllAnalysis[] | null>('/api/v1/Analysis/AllAnalysisList', {
    dateType,
    startTime,
    endTime
  });
  return data;
};
/** 会话列表 */
export const fetchConversationsList = async (PageIndex: number, PageSize: number, QueryString: string | null) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.Conversations> | null>(
    '/api/v1/Chat/ChatConversationList',
    {
      PageSize,
      PageIndex,
      QueryString
    }
  );
  return data;
};
/** 删除会话 */
export const fetchDeleteChatConversation = async (conversationId: number) => {
  return request.get<boolean>('/api/v1/Chat/DeleteChatConversation', { conversationId });
};
/** 修改聊天会话 */
export const fetchChangeChatConversation = async (conversationId: number, conversationName: string) => {
  return request.get<boolean>('/api/v1/Chat/ChangeChatConversation', { conversationId, conversationName });
};
/** 新建聊天会话 */
export const fetchAddChatConversation = async (conversationName: string,knowledgeId: number) => {
  return request.get<ApiGptManagement.Conversations>('/api/v1/Chat/AddChatConversation', { conversationName,knowledgeId });
};

export const fetchChatAPIProcess = async (params: {
  prompt: string;
  // 会话ID
  conversationId: number | null;
  model: string;
  modelType: number;
  contextCount: number | null;
  fileUrl?: string | null;
  // options?: { conversationId?: string; parentMessageId?: string }
  signal?: GenericAbortSignal;
  onDownloadProgress?: (progressEvent: AxiosProgressEvent) => void;
}) => {
  const data: Record<string, any> = {
    prompt: params.prompt,
    conversationId: params.conversationId,
    model: params.model,
    modelType: params.modelType,
    contextCount: params.contextCount,
    fileUrl: params.fileUrl
  };
  return request.post<string>('/api/v1/Chat/ChatCompletionStream', data, {
    signal: params.signal,
    onDownloadProgress: params.onDownloadProgress,
    headers: {
      Accept: 'application/octet-stream',
      'Content-Type': 'application/json'
    }
  });
};
/**
 * 知识库列表
 */
export const fetchKnowledgeList = async () => {
  const data = await request.get<ApiKnowledgeManagement.Knowledge[] | null>('/api/v1/Knowledge/GetList');
  return adapter(adapterOfFetchKnowledgeList, data);
};


/**
 * 向量列表
 */
export const fetchVectorList = async (params:{
  knowledgeId: number,
  id: string | null,
  vector: number[] | null,
  topK: number,
  namespace: string | undefined,
  includeValues: boolean,
  includeMetadata: boolean
}) => {
  const data = await request.post<ApiKnowledgeManagement.Vector | null>(`/${params.knowledgeId}/api/v1/Vector/Query`, {
    id:params.id,
    vector:params.vector,
    topK:params.topK,
    namespace:params.namespace,
    includeValues:params.includeValues,
    includeMetadata:params.includeMetadata
  });
  return adapter(adapterOfFetchVectorList, data);
};
/**
 * 知识库列表
 */
export const fetchEmbaddingVectorList = async (params:{
  knowledgeId: number,
  filter: any,
  topK: number,
  namespace: string | undefined,
  includeValues: boolean,
  includeMetadata: boolean
}) => {
  const data = await request.post<ApiKnowledgeManagement.Vector | null>(`/${params.knowledgeId}/api/v1/Vector/EmbaddingQuery`, {
    filter:params.filter,
    topK:params.topK,
    namespace:params.namespace,
    includeValues:params.includeValues,
    includeMetadata:params.includeMetadata
  });
  return adapter(adapterOfFetchVectorList, data);
};
/**
 * 查询我的图片
 * @param queryString
 * @param pageIndex
 * @param pageSize
 */
export const MyImageList = async (
    queryString: string | null,
    pageIndex: number | undefined,
    pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.ImageRes> | null>(
      '/api/v1/Image/MyImageList',
      { queryString, pageIndex, pageSize }
  );
  return data;
};
/**
 * 图片广场
 * @param queryString
 * @param pageIndex
 * @param pageSize
 */
export const ShareImageList = async (
    queryString: string | null,
    pageIndex: number | undefined,
    pageSize: number | undefined
) => {
  const data = await request.post<ApiGptManagement.PageData<ApiGptManagement.ImageRes> | null>(
      '/api/v1/Image/ShareImageList',
      { queryString, pageIndex, pageSize }
  );
  return data;
};
