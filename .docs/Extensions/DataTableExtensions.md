# DataTableExtensions �๦���ĵ�

## ����

[DataTableExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L8-L168) ��һ����̬��չ�࣬Ϊ `DataTable` �����ṩ�˷ḻ����չ�����������������ת������ѯɸѡ���ṹ���������ݴ���ȶ��ֹ��ܣ�ּ�ڼ� DataTable �ĳ��ò�����������ݴ����Ч�ʺʹ���ɶ��ԡ�

## ��Ҫ����ģ��

### 1. ״̬�жϷ���

�ṩ DataTable ״̬���ı�ݷ�����

**��Ҫ������**
- [IsNullOrEmpty()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L13-L15) - �ж� DataTable �Ƿ�Ϊ null ����������

### 2. ����ת������

�ṩ DataTable ���������ݽṹ��ת�����ܡ�

**��Ҫ������**
- [ToList<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L23-L27) - �� DataTable ת��Ϊ���ͼ���
- [ToDictionaryList()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L51-L66) - ��ȡ DataTable �����������ݣ�ÿ��Ϊ�ֵ䣩
- [AsEnumerableDictionary()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L107-L120) - DataTable תΪһ��һ�ֵ��ö�٣����ڱ�����
- [ToArray()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L159-L167) - DataTable תΪ��ά����

### 3. Ԫ���ݻ�ȡ����

�ṩ��ȡ DataTable �ṹ��Ϣ�Ĺ��ܡ�

**��Ҫ������**
- [GetColumnNames()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L35-L39) - ��ȡ DataTable ����������

### 4. ����ɸѡ�����򷽷�

�ṩ���ݲ�ѯ�������ܡ�

**��Ҫ������**
- [Filter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L74-L83) - DataTable ������ɸѡ�������� DataTable
- [Sort()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L91-L100) - DataTable ��ָ�������򣬷����� DataTable

### 5. �ṹ��������

�ṩ DataTable �ṹ��¡�͸��ƹ��ܡ�

**��Ҫ������**
- [CopyAll()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L127-L130) - DataTable ��¡�ṹ��������������
- [CloneStructure()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L137-L140) - DataTable ��¡�ṹ������������

### 6. ���ݲ�������

�ṩ�м����ݲ������ܡ�

**��Ҫ������**
- [AddRow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L147-L153) - DataTable ���һ������
- [ClearRows()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L159-L161) - DataTable ɾ��������

## ʹ�ó���

1. **����ת��** - �� DataTable ת��Ϊ���󼯺ϡ��ֵ��б���������ݽṹ
2. **���ݲ�ѯ** - �� DataTable ����ɸѡ���������
3. **���ݵ���** - �� DataTable ���ݵ���Ϊ�����������ʽ
4. **���ݴ���** - ���� DataTable �����ݽ�����������
5. **Web API** - �� DataTable ת��Ϊ JSON ��ʽ����
6. **��������** - ����������Դ�� DataTable ����
7. **����Ǩ��** - �ڲ�ͬ���ݽṹ��ת������
8. **��Ԫ����** - �򻯲����ж� DataTable ���ݵ���֤�Ͳ���

## ע������

1. ���з���������չ��������Ҫͨ�� `DataTable` ʵ������
2. �󲿷ַ������� null ֵ�����˰�ȫ���������׳��쳣
3. [ToList<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L23-L27) ������Ҫ�ṩ��ת��ί����ָ��ת���߼�
4. [Filter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L74-L83) �� [Sort()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L91-L100) ����ʹ�� DataTable ���õ� Select ������֧�ֱ�׼�ı��ʽ�﷨
5. [AsEnumerableDictionary()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L107-L120) ���������ӳ�ִ�е�ö�������ʺϴ�����������
6. [AddRow()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L147-L153) ��������˳�����ֵ����Ҫע�����˳������˳��ƥ��
7. [ToArray()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DataTableExtensions.cs#L159-L167) �������� object ���͵Ķ�ά���飬ʹ��ʱ������Ҫ����ת��
8. ��¡�͸��Ʒ������� DataTable ���õ� Copy �� Clone ����ʵ��