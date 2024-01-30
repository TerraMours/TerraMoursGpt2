import { getServiceEnvConfig } from '~/.env-config';
import { createRequest } from './request';

const { url } = getServiceEnvConfig(import.meta.env);

const htmlElement = document.querySelector('html');
const envBaseUrl = htmlElement ? htmlElement.getAttribute('env_now') : null;
// 优先获取环境变量中的值，没有传再获取envconfig的值
export const baseUrl = envBaseUrl !== null ? envBaseUrl : url;

export const request = createRequest({ baseURL: envBaseUrl !== null ? envBaseUrl : url });

export const mockRequest = createRequest({ baseURL: '/mock' });
