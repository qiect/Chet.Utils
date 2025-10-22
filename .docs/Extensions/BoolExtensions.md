# BoolExtensions �๦���ĵ�

## ����

[BoolExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L7-L179) ��һ����̬��չ�࣬Ϊ `bool` �����ṩ�˷ḻ����չ��������������жϡ�ת������ʽ�����߼�����ȶ��ֹ��ܣ�ּ�ڼ򻯲���ֵ�Ĵ����ת����������ߴ���Ŀɶ��Ժͱ����ԡ�

## ��Ҫ����ģ��

### 1. �жϷ���

�ṩ����ֵ״̬�жϵı�ݷ�����

**��Ҫ������**
- [IsTrue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L12-L12) - �жϲ���ֵ�Ƿ�Ϊ true
- [IsFalse()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L18-L18) - �жϲ���ֵ�Ƿ�Ϊ false
- [Not()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L24-L24) - �Բ���ֵ����ȡ������

### 2. ��ֵת������

�ṩ����ֵ��������ֵ���͵�ת����

**��Ҫ������**
- [ToInt()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L30-L30) - ת��Ϊ int��true=1, false=0��
- [ToByte()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L126-L126) - ת��Ϊ byte��true=1, false=0��
- [ToShort()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L132-L132) - ת��Ϊ short��true=1, false=0��
- [ToLong()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L138-L138) - ת��Ϊ long��true=1, false=0��
- [ToFloat()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L144-L144) - ת��Ϊ float��true=1.0, false=0.0��
- [ToDouble()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L150-L150) - ת��Ϊ double��true=1.0, false=0.0��
- [ToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L156-L156) - ת��Ϊ decimal��true=1.0, false=0.0��

### 3. �ַ���ת������

�ṩ����ֵ�������ַ�����ʽ��ת����

**��Ҫ������**
- [ToStringValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L36-L36) - ת��Ϊ��׼�ַ�����"True"/"False"��
- [ToChineseString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L42-L42) - ת��Ϊ�����ַ�����"��"/"��"��
- [ToCustomString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L48-L49) - ת��Ϊ�Զ����ַ���
- [ToYesNo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L55-L55) - ת��Ϊ Yes/No �ַ���
- [ToOnOff()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L61-L61) - ת��Ϊ On/Off �ַ���
- [ToOneZero()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L67-L67) - ת��Ϊ 1/0 �ַ���
- [ToYN()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L73-L73) - ת��Ϊ Y/N �ַ���
- [ToReverseString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L162-L162) - ת��Ϊ�����ַ�����"False"/"True"��
- [ToReverseChineseString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L168-L168) - ת��Ϊ���������ַ�����"��"/"��"��

### 4. �߼����㷽��

�ṩ����ֵ���߼����������

**��Ҫ������**
- [And()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L79-L79) - �����㣨AND��
- [Or()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L85-L85) - �����㣨OR��
- [Xor()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L91-L91) - ������㣨XOR��
- [Xnor()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L97-L97) - ͬ�����㣨�ȼ�����ȱȽϣ�

### 5. ����ӳ�䷽��

�ṩ����ֵ���������͵�ӳ��ת����

**��Ҫ������**
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L103-L104) - ת��Ϊö��ֵ
- [ToNullable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L110-L111) - ת��Ϊ�ɿղ���ֵ
- [ToValue<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L117-L118) - ת��Ϊָ�����͵�ֵ

## ʹ�ó���

1. **������ʾ** - ������ֵת��Ϊ�û��Ѻõ��ַ�����ʽ��������"��/��"��
2. **����ת��** - �����ݴ����н�����ֵת��Ϊ���ֻ���������
3. **���ù���** - ������������Ķ��ֱ�ʾ��ʽ
4. **�߼�����** - ʹ����ʽ���ý��и��ӵĲ����߼�����
5. **API����** - ������ֵת��Ϊ�ض���ʽ���ַ�������Ӧ�ӿ�Ҫ��
6. **���ʻ�** - ֧�ֶ����Ի����µĲ���ֵ��ʾ
7. **����ӳ��** - ������ֵӳ�䵽ö�ٻ�����ҵ�����

## ע������

1. ���з���������չ��������Ҫͨ�� `bool` ʵ������
2. ��ֵת��������ѭ��׼Լ����true ӳ��Ϊ 1���� 1.0����false ӳ��Ϊ 0���� 0.0��
3. �ַ���ת�������ṩ�˶��ֳ�����ʽ��Ҳ��ͨ���Զ��巽��ָ���ض���ʽ
4. �߼����㷽���ṩ�˺���ʽ��̷��ĵ��÷�ʽ
5. [ToNullable()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L110-L111) ����Ĭ�Ϸ���ԭֵ��ֻ�е� `nullable` ����Ϊ true ʱ�ŷ��� null
6. [ToValue<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\BoolExtensions.cs#L117-L118) ����֧�ֽ�����ֵӳ�䵽��������
7. ����ת�������ṩ�볣��ת���෴�Ľ��