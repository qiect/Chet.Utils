# DataTableHelper �๦���ĵ�

## ����

[DataTableHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L12-L1893) ��һ����̬�����࣬�ṩ�˷ḻ�� `DataTable` ���ݴ���ת���������͵������ܡ�����ּ�ڼ򻯸��ӵ����ݲ�������������ת��ӳ�䡢͸�ӷ��顢ͳ�Ʒ��������ݵ�����������֤��ϴ���������Ӻϲ��Լ��߼���ѯɸѡ�ȡ�

## ��Ҫ����ģ��

### 1. ����ת����ӳ��

�ṩ�������ݸ�ʽ֮���ת�����ܣ�֧�ֽ� `DataTable` ת��Ϊ��̬����ָ�����Ͷ����ֵ��б�ȣ�Ҳ֧�ַ���ת��������ӳ�䡣

**��Ҫ������**
- [ToDynamicList()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L24-L43) - �� `DataTable` ת��Ϊ��̬�����б�
- [ToObjectList<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L52-L88) - �� `DataTable` ת��Ϊָ�����͵Ķ����б�
- [ToDataTable<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L97-L125) - �������б�ת��Ϊ `DataTable`
- [ToDataTableFromJson()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L134-L183) - �� JSON �ַ���ת��Ϊ `DataTable`
- [ToDictionaryList()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L192-L210) - �� `DataTable` ת��Ϊ�ֵ��б�
- [MapColumns()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L219-L234) - ��̬ӳ�� `DataTable` ����

### 2. ����͸�������

�ṩ����͸�Ӻͷ���ۺϹ��ܣ�֧�ִ������ӵĽ����ͻ��ܱ���

**��Ҫ������**
- [Pivot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L246-L304) - ����͸�Ӳ���
- [GroupBy()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L314-L357) - ���ݷ���ۺ�
- [AdvancedPivot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L367-L452) - �����߼�����͸�ӱ�

### 3. ���ݷ�����ͳ��

�ṩ����ͳ�Ʒ������ܣ���������ͳ��ָ����㡢�ֲ�����������Է�����

**��Ҫ������**
- [CalculateStatistics()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L484-L508) - ��������ͳ����Ϣ
- [AnalyzeDistribution()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L585-L630) - ���ݷֲ�����
- [CalculateCorrelation()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L690-L719) - ����Է���

### 4. ���ݵ��������л�

֧�ֽ� `DataTable` ����Ϊ���ָ�ʽ������ CSV��JSON��HTML �� Excel XML��

**��Ҫ������**
- [ExportToCsv()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L731-L751) - ����Ϊ CSV ��ʽ
- [ExportToJson()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L762-L794) - ����Ϊ JSON ��ʽ
- [ExportToHtml()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L805-L835) - ����Ϊ HTML ���
- [ExportToExcelXml()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L845-L882) - ����Ϊ Excel XML ��ʽ

### 5. ������֤����ϴ

�ṩ�����������ƹ��ܣ�����������֤����ϴ��ȥ�ء�ȱʧֵ�����쳣ֵ��⡣

**��Ҫ������**
- [ValidateData()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L905-L935) - ��֤ `DataTable` ����
- [CleanData()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L945-L962) - ��ϴ `DataTable` ����
- [RemoveDuplicates()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L971-L988) - ȥ���ظ���
- [FillMissingValues()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L997-L1018) - ���ȱʧֵ
- [DetectOutliers()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1027-L1074) - ����쳣ֵ

### 6. ����������ϲ�

֧�ֶ������ݱ����Ӻͺϲ����������������ӡ������Ӻʹ�ֱ/ˮƽ�ϲ���

**��Ҫ������**
- [InnerJoin()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1086-L1136) - ���������� `DataTable`
- [LeftJoin()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1147-L1213) - ���������� `DataTable`
- [UnionAll()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1223-L1237) - �ϲ���� `DataTable`����ֱ�ϲ���
- [MergeHorizontally()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1247-L1331) - �ϲ���� `DataTable`��ˮƽ�ϲ���

### 7. �߼���ѯ��ɸѡ

�ṩ���� SQL �Ĳ�ѯ���ܺ͸߼�ɸѡ������

**��Ҫ������**
- [Query()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1343-L1382) - ִ�� SQL ����ѯ
- [Filter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1459-L1471) - ִ�и���ɸѡ
- [Search()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1482-L1507) - ִ��ȫ������
- [RegexFilter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1517-L1535) - ִ��������ʽɸѡ

## ���ݽṹ

### ͳ�Ʒ�����
- [DataStatistics](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1549-L1615) - ����ͳ����Ϣ
- [DataDistribution](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1620-L1637) - ���ݷֲ���Ϣ
- [HistogramBin](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1642-L1663) - ֱ��ͼ��

### ��֤���쳣������
- [ValidationResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L891-L896) - ������֤���

### ö����
- [OutlierDetectionMethod](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1668-L1681) - �쳣ֵ��ⷽ����IQR��ZScore��

### �ڲ�������
- [ArrayEqualityComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1686-L1722) - ������ȱȽ���
- [ObjectEqualityComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1727-L1747) - ������ȱȽ���
- [ObjectComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1752-L1777) - ����Ƚ���
- [DataRowComparer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\DataTableHelper.cs#L1782-L1816) - DataRow �Ƚ���

## ʹ�ó���

1. **����ת��** - �ڲ�ͬ���ݸ�ʽ֮�����ת��
2. **��������** - ����͸�ӱ�ͻ��ܱ���
3. **���ݷ���** - ����ͳ�Ʒ����������ھ�
4. **���ݵ���** - �����ݵ���Ϊ���ָ�ʽ������ϵͳʹ��
5. **������ϴ** - ������������������쳣ֵ��ȱʧֵ
6. **���ݼ���** - �ϲ����Բ�ͬ����Դ������
7. **���ݲ�ѯ** - ���ڴ��е����ݱ�ִ�и��Ӳ�ѯ

## ע������

1. ���ַ���������Ҫ�����ڴ洦������ݼ�
2. ĳЩͳ�Ʒ�������Ҫ��������Ϊ��ֵ����
3. ������ʽɸѡ����Ӱ������
4. JSON ���������� `System.Text.Json` ��
5. �߼���ѯ���ܵ� WHERE �Ӿ������Ϊ�򵥣����ӱ��ʽ���ܲ�֧��