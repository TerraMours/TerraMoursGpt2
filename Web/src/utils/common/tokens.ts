export function countTokens(str: string) {
  // 使用正则表达式匹配汉字、英文字母、数字和标点符号，计算数量
  const chineseRegex = /[\u4e00-\u9fa5]/g;
  const chineseMatches = str.match(chineseRegex);
  const chineseTokenCount = chineseMatches ? chineseMatches.length * 2 : 0;
  const englishRegex = /[a-zA-Z]/g;
  const englishMatches = str.match(englishRegex);
  const englishTokenCount = englishMatches ? englishMatches.length : 0;
  const numberRegex = /[0-9]/g;
  const numberMatches = str.match(numberRegex);
  const numberTokenCount = numberMatches ? numberMatches.length : 0;
  const punctuationRegex = /[,.?!;:，。？！；：]/g;
  const punctuationMatches = str.match(punctuationRegex);
  const punctuationTokenCount = punctuationMatches ? punctuationMatches.length : 0;
  // 返回总的token数量
  return chineseTokenCount + englishTokenCount + numberTokenCount + punctuationTokenCount;
}
/**
 * 复制文本
 * @param options
 */
export function copyText(options: { text: string; origin?: boolean }) {
  const props = { origin: true, ...options };

  let input: HTMLInputElement | HTMLTextAreaElement;

  if (props.origin) input = document.createElement('textarea');
  else input = document.createElement('input');

  input.setAttribute('readonly', 'readonly');
  input.value = props.text;
  document.body.appendChild(input);
  input.select();
  if (document.execCommand('copy')) document.execCommand('copy');
  document.body.removeChild(input);
}
