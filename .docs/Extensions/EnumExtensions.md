# EnumExtensions �๦���ĵ�

## ����

[EnumExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L8-L207) ��һ����̬��չ�࣬Ϊö�������ṩ�˷ḻ����չ�������������ö��ֵ�жϡ�ת����������ȡ����־λ�����ȶ��ֹ��ܣ�ּ�ڼ�ö�����͵�ʹ�ã���ߴ���Ŀɶ��Ժͱ����ԣ��ر���������Ҫ����ö����������־λö�ٵȸ��ӳ�����

## ��Ҫ����ģ��

### 1. ö��ֵ�жϷ���

�ṩö��ֵ��Ч�Լ�鹦�ܡ�

**��Ҫ������**
- [IsDefined<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L13-L14) - �ж�ö��ֵ�Ƿ�����ö��������

### 2. ö��������Ϣ��ȡ����

�ṩö������Ԫ���ݻ�ȡ���ܡ�

**��Ҫ������**
- [GetValues<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L20-L21) - ��ȡö����������ֵ�б�
- [GetNames<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L27-L28) - ��ȡö���������������б�
- [GetUnderlyingType<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L203-L204) - ��ȡö�����͵Ļ������ͣ��� int��byte��

### 3. ö��ֵת������

�ṩö��ֵ�������������֮����໥ת����

**��Ҫ������**
- [ToInt<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L34-L35) - ö��ֵתΪ int
- [ToLong<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L41-L42) - ö��ֵתΪ long
- [ToStringValue<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L48-L49) - ö��ֵתΪ�ַ���
- [Parse<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L88-L96) - �ַ���תΪö��ֵ
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L104-L110) - int תΪö��ֵ
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L117-L132) - long תΪö��ֵ

### 4. �������Դ�����

�ṩ���� DescriptionAttribute ��ö�����������ܡ�

**��Ҫ������**
- [GetDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L55-L63) - ö��ֵתΪ������DescriptionAttribute�����������򷵻�����
- [FromDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L70-L81) - ����������ȡö��ֵ��DescriptionAttribute��
- [GetValueDescriptionDict<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L138-L146) - ��ȡö����������ֵ���������ֵ�
- [GetNameDescriptionDict<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L152-L160) - ��ȡö�������������Ƽ��������ֵ�

### 5. ��־λö�ٲ�������

�ṩ��� [Flags] ����ö�ٵ�λ�������ܡ�

**��Ҫ������**
- [HasFlag<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L166-L167) - �ж�ö��ֵ�Ƿ����ָ����־
- [AddFlag<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L173-L177) - ö��ֵ��ӱ�־
- [RemoveFlag<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L183-L187) - ö��ֵ�Ƴ���־

## ʹ�ó���

1. **UI ��ʾ** - ��ö��ֵת��Ϊ�û��Ѻõ������ı���ʾ
2. **���ݵ��뵼��** - ����ö��ֵ���ַ���������֮����໥ת��
3. **���ù���** - ��ȡ�ͱ���ö�����͵�������
4. **Ȩ�޿���** - ʹ�ñ�־λö�ٽ���Ȩ��λ����
5. **API ����** - ö��ֵ���ⲿϵͳ���ݸ�ʽ��ת��
6. **��־��¼** - ��¼ö��ֵ��������Ϣ������ֵ
7. **���ݰ�** - Ϊ�����б�ȿؼ��ṩö��ֵ�������İ�����
8. **ҵ���߼�** - ����ö����������ҵ������ж�

## ע������

1. ���з���������չ��������Ҫͨ��ö��ʵ�������͵���
2. [GetDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L55-L63) ���������� DescriptionAttribute ���ԣ�δ��ǵ�ö��ֵ����������
3. [Parse<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L88-L96) ����֧�ִ�Сд�����е��ַ�������
4. [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L104-L110) �� [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L117-L132) ������ת��ʧ��ʱ����Ĭ��ֵ���������׳��쳣
5. ��־λ�������������ڴ��� [Flags] ���Ե�ö������
6. [FromDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L70-L81) �������Ҳ���ƥ������ʱ����Ĭ��ö��ֵ
7. �ֵ��ȡ�������������ݰ󶨺�ö��ֵ��������
8. �������ͻ�ȡ�����������˽�ö�ٵĵײ�洢����
9. ����ת���������Ա߽����������˰�ȫ������ߴ��뽡׳��