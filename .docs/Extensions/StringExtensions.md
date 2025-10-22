# StringExtensions �๦���ĵ�

## ����

[StringExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L8-L504) ��һ����̬��չ�࣬Ϊ `string` �����ṩ�˷ḻ����չ��������������ַ����жϡ�������ʽ��֤������ת�����ַ��������ȶ��ֹ��ܣ�ּ�ڼ��ַ��������������ߴ���İ�ȫ�ԺͿɶ��ԣ��ر���������Ҫ��������ַ������͵��ճ�����������

## ��Ҫ����ģ��

### 1. �ַ����жϷ���

�ṩ�ַ���״̬����������֤�ı�ݷ�����

**��Ҫ������**
- [IsNullOrEmpty()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L15-L15) - �ж��ַ����Ƿ�Ϊ null ����ַ���
- [IsNullOrWhiteSpace()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L21-L21) - �ж��ַ����Ƿ�Ϊ null ��������հ��ַ�
- [IsNumeric()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L27-L28) - �ж��ַ����Ƿ�Ϊ���֣��ɽ���Ϊ double��
- [IsInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L34-L35) - �ж��ַ����Ƿ�Ϊ����
- [IsFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L41-L42) - �ж��ַ����Ƿ�Ϊ��������float��
- [IsDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L48-L49) - �ж��ַ����Ƿ�Ϊʮ��������decimal��
- [IsGuid()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L55-L56) - �ж��ַ����Ƿ�Ϊ Guid
- [EqualsIgnoreCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L62-L63) - ���Դ�Сд�ж��ַ����Ƿ����
- [IsChinese()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L69-L72) - �ж��ַ��Ƿ�Ϊ�����ַ�
- [HasChinese()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L78-L81) - �ж��ַ������Ƿ��������
- [IsNull()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L87-L104) - �ж��ַ����Ƿ�Ϊ�գ�֧���Զ�����ַ�����

### 2. ������ʽ��֤����

�ṩ����������ʽ���ַ�����ʽ��֤���ܡ�

**��Ҫ������**
- [IsLetterByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L113-L116) - ��֤�ַ��Ƿ�����ĸ����
- [IsNumByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L121-L124) - ��֤�ַ��Ƿ�����������
- [ExtractNumByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L129-L132) - ��ȡ�ַ����е����ֲ���
- [IsFloatByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L137-L140) - ��֤�ַ��ǲ��Ǹ�������
- [IsEmailByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L145-L148) - ��֤�ַ��Ƿ���Email��ʽ
- [IsTelByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L153-L156) - ��֤�ַ��Ƿ���Tel��ʽ
- [IsMobileByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L161-L164) - ��֤�Ƿ����ֻ�����
- [IsUrlByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L169-L172) - ��֤�Ƿ�����ַ
- [IsDateByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L177-L180) - ��֤�ַ����Ƿ�����������
- [IsTimeByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L185-L188) - ��֤�ַ����Ƿ���ʱ������
- [IsDateTimeByRegex()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L193-L196) - ��֤�ַ����Ƿ�������ʱ������

### 3. ����ת������

�ṩ�ַ����������������͵�ת�����ܡ�

**��Ҫ������**
- [ToInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L205-L206) - �ַ���ת int��ʧ�ܷ���Ĭ��ֵ
- [ToFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L212-L213) - �ַ���ת float��ʧ�ܷ���Ĭ��ֵ
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L219-L220) - �ַ���ת double��ʧ�ܷ���Ĭ��ֵ
- [ToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L323-L324) - �ַ���ת decimal��ʧ�ܷ���Ĭ��ֵ
- [ToBool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L330-L331) - �ַ���ת bool��ʧ�ܷ���Ĭ��ֵ
- [ToGuid()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L337-L338) - �ַ���ת Guid��ʧ�ܷ���Ĭ��ֵ
- [ToDateTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L344-L345) - �ַ���ת DateTime��ʧ�ܷ���Ĭ��ֵ

#### ��ֵ��Լת������
- [ToDoubleRound()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L226-L231) - �ַ���ת double������ָ��С��λ����������
- [ToDoubleTruncate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L237-L245) - �ַ���ת double������ָ��С��λ������ȡ��
- [ToFloatRound()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L251-L256) - �ַ���ת float������ָ��С��λ����������
- [ToFloatTruncate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L262-L270) - �ַ���ת float������ָ��С��λ������ȡ��
- [KeepDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L276-L281) - ������ֵ���ַ�����С��λ

### 4. �ַ�����������

�ṩ�ַ��������ת�����ܡ�

**��Ҫ������**
- [TrimSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L354-L354) - ��ȫȥ���ַ�����β�հ��ַ�
- [RemoveWhiteSpace()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L360-L361) - �Ƴ��ַ����е����пհ��ַ�
- [SubstringSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L367-L373) - ��ȫ��ȡ�ַ���
- [Left()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L379-L383) - ��ȡ�ַ������ָ�����ȵ��Ӵ�
- [Right()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L389-L393) - ��ȡ�ַ����Ҳ�ָ�����ȵ��Ӵ�
- [Reverse()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L399-L405) - ��ת�ַ���
- [RemoveSpecialChars()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L411-L414) - �Ƴ��ַ����е������ַ�
- [ToCamelCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L420-L424) - תΪ camelCase������ĸСд��
- [ToPascalCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L430-L434) - תΪ PascalCase������ĸ��д��
- [Repeat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L440-L444) - �ظ��ַ���ָ������
- [ReplaceIgnoreCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L450-L454) - ���Դ�Сд�滻�ַ����е�ָ������
- [ContainsIgnoreCase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L467-L471) - �ж��ַ����Ƿ����ָ���Ӵ������Դ�Сд
- [SplitSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L477-L481) - ���ַ�����ָ���ָ����ָ�Ϊ�ַ�������

### 5. ��ϣ���㷽��

�ṩ�ַ�����ϣֵ���㹦�ܡ�

**��Ҫ������**
- [ToMd5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\StringExtensions.cs#L460-L465) - ��ȡ�ַ����� MD5 ֵ��32λСд��

## ʹ�ó���

1. **������֤** - ��֤�û�����ĸ�ʽ�Ƿ���ȷ�����䡢�ֻ��š���ַ�ȣ�
2. **����ת��** - ��ȫ�ؽ��ַ���ת��Ϊ������������
3. **�ַ�������** - �ַ�����ȡ��ƴ�ӡ���ʽ���Ȳ���
4. **������ϴ** - �Ƴ������ַ����հ��ַ�������Ԥ����
5. **��ʽ��֤** - ��֤�����Ƿ�����ض���ʽҪ��
6. **�ı�����** - �ж��ı��������ͣ����ġ����֡���ĸ�ȣ�
7. **��ȫ����** - ���� null �쳣�İ�ȫ�ַ�������
8. **����ת��** - �ַ�����������������֮����໥ת��

## ע������

1. ���з���������չ��������Ҫͨ�� `string` ʵ������
2. ���� "Safe" ��׺�ķ������� null ֵ�����˰�ȫ���������׳��쳣
3. ����ת��������ת��ʧ��ʱ����ָ����Ĭ��ֵ���������׳��쳣
4. ������ʽ��֤����ʹ��Ԥ���������ģʽ���и�ʽ��֤
5. ��ֵ��Լ����֧���������������ȡ�����ֲ���
6. �ַ�����������֧�ֱ߽��飬��������Խ���쳣
7. ��ϣ���㷽��ʹ�� UTF-8 ��������ֽ�ת��
8. �����жϷ������� Unicode �ַ���Χ����ʶ��
9. �Զ�����ַ����ж�֧�ֶ��ֿ�ֵ����
10. ���з����������������������Ч�Լ��