# StreamExtensions �๦���ĵ�

## ����

[StreamExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L8-L255) ��һ����̬��չ�࣬Ϊ `Stream` �����ṩ�˷ḻ����չ����������������Ķ�ȡ��д�롢ת�����жϡ������ȶ��ֹ��ܣ�ּ�ڼ��������������ߴ���İ�ȫ�ԺͿɶ��ԣ��ر���������Ҫ������������͵��ճ�����������

## ��Ҫ����ģ��

### 1. ��״̬�жϷ���

�ṩ�����Ժ�״̬���ı�ݷ�����

**��Ҫ������**
- [CanReadSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L13-L13) - �ж����Ƿ�ɶ�
- [CanWriteSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L19-L19) - �ж����Ƿ��д
- [CanSeekSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L25-L25) - �ж����Ƿ�ɲ��ң�֧�� Seek��
- [IsNullOrEmpty()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L31-L31) - �ж����Ƿ�Ϊ�ջ򳤶�Ϊ��
- [IsMemoryStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L209-L209) - �ж����Ƿ�Ϊ MemoryStream
- [IsFileStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L215-L215) - �ж����Ƿ�Ϊ FileStream
- [IsNullStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L221-L221) - �ж����Ƿ�Ϊ������Stream.Null��

### 2. ��ת������

�ṩ����������������֮����໥ת�����ܡ�

**��Ҫ������**
- [ToBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L37-L52) - �������ݶ�ȡΪ�ֽ�����
- [ToText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L58-L62) - �������ݶ�ȡΪ�ַ�����Ĭ�� UTF8 ���룩
- [ToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L121-L125) - ���ļ����ݶ�ȡΪ��
- [ToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L131-L136) - ���ַ���ת��Ϊ����Ĭ�� UTF8 ���룩
- [ToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L142-L146) - ���ֽ�����ת��Ϊ��

### 3. ��д�뷽��

�ṩ����д�����ݵĹ��ܡ�

**��Ҫ������**
- [WriteText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L68-L77) - ���ַ���д����������ԭ���ݣ�Ĭ�� UTF8 ���룩
- [WriteBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L83-L91) - ���ֽ�����д����������ԭ���ݣ�

### 4. ���ļ���������

�ṩ�����ļ�ϵͳ֮��Ľ������ܡ�

**��Ҫ������**
- [SaveToFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L97-L103) - �������ݱ��浽�ļ������ǣ�

### 5. �����Ʒ���

�ṩ��֮������ݸ��ƹ��ܡ�

**��Ҫ������**
- [CopyToStream()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L152-L159) - �������ݸ��Ƶ���һ����

### 6. ��λ�ò�������

�ṩ��λ�ÿ��ƺͲ��ֶ�ȡ���ܡ�

**��Ҫ������**
- [ResetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L165-L168) - ����������ʼλ��
- [ReadBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L174-L191) - ��ȡ���Ĳ�������Ϊ�ֽ�����
- [ReadText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L197-L203) - ��ȡ���Ĳ�������Ϊ�ַ�����Ĭ�� UTF8 ���룩
- [GetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L233-L233) - ��ȡ���ĵ�ǰλ��
- [SetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L239-L244) - �������ĵ�ǰλ��

### 7. ����Ϣ��ȡ����

�ṩ��Ԫ���ݻ�ȡ���ܡ�

**��Ҫ������**
- [GetLength()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L227-L227) - ��ȡ���ĳ��ȣ��ֽڣ�

### 8. ���ͷŷ���

�ṩ��ȫ�����رպ��ͷŹ��ܡ�

**��Ҫ������**
- [CloseSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L250-L254) - �رղ��ͷ���

## ʹ�ó���

1. **�ļ�����** - ��д�ļ��������������ļ������ļ�������
2. **����ͨ��** - ���������������Ķ�ȡ��д��
3. **����ת��** - �����ַ������ֽ�����֮����໥ת��
4. **�ڴ����** - MemoryStream �Ĵ����Ͳ���
5. **���ݴ���** - ��֮������ݸ��ƺʹ���
6. **���ݽ���** - ��ȡ���Ĳ������ݽ��н�������
7. **���봦��** - ��ͬ�����ʽ���ı�������
8. **��Դ����** - ��ȫ�عرպ��ͷ�����Դ

## ע������

1. ���з���������չ��������Ҫͨ�� `Stream` ʵ������
2. ���� "Safe" ��׺�ķ������� null ֵ�����˰�ȫ���������׳��쳣
3. [ToBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L37-L52) ������� MemoryStream �������Ż���ֱ�ӵ��� ToArray() �����������
4. ��ȡ�����ڲ���ǰ��ᱣ��ͻָ�����ԭʼλ��
5. д�뷽��������������ݣ�SetLength(0)����Ȼ��д��������
6. [ReadBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L174-L191) ���������˶�ȡ��������������ʵ�ʶ�ȡ������
7. [SetPosition()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StreamExtensions.cs#L239-L244) ������λ�ò��������˱߽��飬ȷ������Ч��Χ��
8. ���б�����ط���Ĭ��ʹ�� UTF8 ���룬֧���Զ������
9. �ļ���������ʹ�� using ���ȷ����Դ��ȷ�ͷ�
10. �����Ʒ������Զ�����Դ��λ�õ���ʼλ��