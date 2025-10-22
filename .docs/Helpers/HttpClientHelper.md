# HttpClientHelper �๦���ĵ�

## ����

[HttpClientHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L11-L894) ��һ����̬�����࣬�ṩ�˷ḻ�� HTTP �����ܡ������װ�� .NET �� `HttpClient`���ṩ�˻����� HTTP ���󷽷����߼����ԣ������Ի��ơ���ʱ���ơ���֤�ȣ����ļ��ϴ����ء�����������ȹ��ܣ�ּ�ڼ� HTTP ͨ�Ų�����

## ��Ҫ����ģ��

### 1. ���� HTTP ���󷽷�

�ṩ��׼�� HTTP ���󷽷������� GET��POST��PUT��DELETE �ȣ�֧�ֶ������ݸ�ʽ��

**��Ҫ������**
- [GetAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L36-L42) - ���� GET ����
- [GetStringAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L52-L57) - ���� GET ���󲢷����ַ�������
- [GetByteArrayAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L67-L72) - ���� GET ���󲢷����ֽ���������
- [PostAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L83-L91) - ���� POST ����
- [PostJsonAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L101-L106) - ���� POST ����JSON ���ݣ�
- [PostFormAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L116-L120) - ���� POST ���󣨱����ݣ�
- [PutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L130-L138) - ���� PUT ����
- [PutJsonAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L148-L153) - ���� PUT ����JSON ���ݣ�
- [DeleteAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L162-L167) - ���� DELETE ����
- [SendAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L178-L186) - �����Զ��� HTTP ����

### 2. �߼� HTTP ���󷽷�

�ṩ��ǿ���ܵ� HTTP ���󷽷����������Ի��ơ���ʱ���ơ���֤���ļ��ϴ����صȡ�

**��Ҫ������**
- [GetWithRetryAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L198-L202) - ���ʹ����Ի��Ƶ� GET ����
- [PostWithRetryAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L213-L218) - ���ʹ����Ի��Ƶ� POST ����
- [GetWithTimeoutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L228-L233) - ���ʹ���ʱ���Ƶ� GET ����
- [PostWithTimeoutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L244-L249) - ���ʹ���ʱ���Ƶ� POST ����
- [GetWithAuthAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L260-L265) - ���ʹ���֤�� GET ����
- [GetWithBasicAuthAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L277-L282) - ���ʹ�������֤�� GET ����
- [DownloadFileAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L294-L344) - �����ļ�
- [UploadFileAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L356-L403) - �ϴ��ļ�

### 3. ���ú͹��߷���

�ṩ HTTP �ͻ������úͳ��õĹ��߷�����

**��Ҫ������**
- [SetDefaultHeaders()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L413-L420) - ����Ĭ������ͷ
- [SetDefaultTimeout()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L426-L429) - ����Ĭ�ϳ�ʱʱ��
- [SetAutomaticDecompression()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L435-L439) - ���û�����Զ���ѹ��
- [SetCookieContainer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L445-L448) - ���� Cookie ����
- [GetCookieContainer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L454-L457) - ��ȡ Cookie ����
- [ClearDefaultHeaders()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L462-L465) - ���Ĭ������ͷ
- [SerializeToJson<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L509-L516) - ���л�����Ϊ JSON
- [DeserializeFromJson<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L524-L531) - �����л� JSON Ϊ����
- [GetHttpClient()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L537-L540) - ��ȡ HTTP �ͻ���ʵ��

### 4. ����������

�ṩ�������� HTTP ����Ĺ��ܣ�֧�ֲ��к�˳��ִ�С�

**��Ҫ������**
- [SendBatchAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L551-L599) - ���з��Ͷ�� HTTP ����
- [SendSequentialAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L609-L644) - ˳���Ͷ�� HTTP ����

### 5. ��غ�ͳ��

�ṩ HTTP �ͻ��˵ļ�غ�ͳ�ƹ��ܡ�

**��Ҫ������**
- [GetStatistics()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L654-L666) - ��ȡ HTTP �ͻ���ͳ����Ϣ
- [EnableLogging()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L673-L677) - ����������־��¼

## ���ݽṹ

### �����
- [DownloadProgress](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L700-L717) - ���ؽ�����Ϣ
- [DownloadResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L722-L744) - ���ؽ��
- [UploadProgress](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L749-L766) - �ϴ�������Ϣ
- [UploadResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L771-L793) - �ϴ����
- [BatchRequest](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L798-L819) - ��������
- [BatchResponse](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L824-L850) - ������Ӧ
- [HttpClientStatistics](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\HttpClientHelper.cs#L855-L892) - HTTP �ͻ���ͳ����Ϣ

## ʹ�ó���

1. **Web API ����** - ���� RESTful API �Ľ���
2. **�ļ�����** - ʵ���ļ����ϴ������ع���
3. **��������** - ���л�˳������ HTTP ����
4. **��ȫͨ��** - ֧�ָ�����֤���Ƶ� HTTP ����
5. **�ɿ�ͨ��** - ͨ�����Ի����������ɹ���
6. **�����Ż�** - ͨ�������Ż� HTTP �ͻ�������

## ע������

1. ����ʹ�þ�̬ `HttpClient` ʵ������ѭ .NET ���ʵ��
2. Ĭ�������� GZip �� Deflate �Զ���ѹ��
3. Ĭ�ϳ�ʱʱ��Ϊ 30 ��
4. ���Ի��ƻ�����ĳЩ״̬�루�� 400��401��403����������
5. �ļ��ϴ�������֧�ֽ��Ȼص�
6. ��������֧�ֲ�������
7. JSON ���л�/�����л�ʹ�� camelCase ��������
8. ���ָ߼����ܣ�����־��¼����Ҫ����ʵ��