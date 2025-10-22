# EnumerableExtensions �๦���ĵ�

## ����

[EnumerableExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L8-L395) ��һ����̬��չ�࣬Ϊ `IEnumerable<T>` �� `ICollection<T>` �����ṩ�˷ḻ����չ������������������жϡ�ת����������ͳ�Ƶȶ��ֹ��ܣ�ּ�ڼ򻯼��ϲ�������ߴ���İ�ȫ�ԺͿɶ��ԣ��ر������ڴ�����ּ������͵��ճ�����������

## ��Ҫ����ģ��

### 1. IEnumerable ��չ����

�ṩ��Կ�ö�ټ��ϵĸ��ְ�ȫ����������

#### ״̬�жϷ���
- [IsNullOrEmpty<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L16-L17) - �жϼ����Ƿ�Ϊ null ���
- [IsNotEmpty<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L23-L24) - �жϼ����Ƿ�Ϊ��

#### ��ȫ��������
- [SafeCount<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L30-L31) - ��ȡ����Ԫ����������ȫ��
- [FirstOrDefaultSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L37-L38) - ��ȡ���ϵĵ�һ��Ԫ�أ���Ϊ���򷵻�Ĭ��ֵ
- [LastOrDefaultSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L44-L45) - ��ȡ���ϵ����һ��Ԫ�أ���Ϊ���򷵻�Ĭ��ֵ
- [ContainsSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L52-L53) - �жϼ����Ƿ����ָ��Ԫ�أ�֧�� null ���ϣ�

#### ת������
- [ToListSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L59-L60) - ������ת��Ϊ List����ȫ��
- [ToArraySafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L66-L67) - ������ת��Ϊ���飨��ȫ��
- [ToDictionarySafe<TSource, TKey, TValue>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L223-L230) - ����תΪ�ֵ䣨��ȫ��
- [ToHashSetSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L236-L237) - ����תΪ HashSet����ȫ��
- [ToDataTable<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L316-L331) - IEnumerable תΪ DataTable
- [ToConcurrentDictionary<TKey, TValue>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L337-L345) - IEnumerable<KeyValuePair> תΪ ConcurrentDictionary
- [ToConcurrentDictionary<TSource, TKey, TValue>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L353-L362) - IEnumerable תΪ ConcurrentDictionary���Զ����ֵѡ������

#### ���ϲ�������
- [DistinctSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L73-L74) - ����ȥ�أ���ȫ��
- [WhereSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L80-L81) - ���Ϲ��ˣ���ȫ��
- [SelectSafe<TSource, TResult>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L87-L88) - ����ͶӰ����ȫ��
- [GroupBySafe<TSource, TKey>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L94-L95) - ���Ϸ��飨��ȫ��
- [OrderBySafe<TSource, TKey>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L101-L102) - �������򣨰�ȫ��
- [OrderByDescendingSafe<TSource, TKey>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L108-L109) - �����������򣨰�ȫ��
- [Page<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L116-L120) - ���Ϸ�ҳ����ȫ��
- [RemoveNulls<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L217-L218) - ����ȥ�� null Ԫ��
- [Chunk<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L252-L267) - ���Ϸֿ飨��ָ����С���飩
- [ReverseSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L310-L311) - ���Ϸ�ת����ȫ��

#### ͳ�Ʒ���
- [SumSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L125-L128) - ������ͣ���ȫ��- ֧�� int��double��decimal��float
- [AverageSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L133-L136) - ����ƽ��ֵ����ȫ��- ֧�� int��double��decimal��float
- [MaxSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L141-L144) - �������ֵ����ȫ��- ֧�� int��double��decimal��float
- [MinSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L149-L152) - ������Сֵ����ȫ��- ֧�� int��double��decimal��float

#### ��������
- [ForEachSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L243-L247) - ���ϱ���ִ�в�������ȫ��

#### �����жϷ���
- [AllSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L273-L274) - �����Ƿ�ȫ��������������ȫ��
- [AnySafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L280-L281) - �����Ƿ�������Ԫ��������������ȫ��

### 2. ICollection ��չ����

�ṩ��Լ������͵İ�ȫ����������

#### ״̬�жϷ���
- [IsNullOrEmpty<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L371-L372) - �жϼ����Ƿ�Ϊ null ���

#### Ԫ�ز�������
- [AddSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L379-L382) - ��ȫ���Ԫ�ص�����
- [RemoveSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L388-L391) - ��ȫ�Ƴ�Ԫ��
- [ClearSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L397-L400) - ��ռ��ϣ���ȫ��
- [AddRangeSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L407-L411) - �����������Ԫ�أ���ȫ��
- [RemoveRangeSafe<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L417-L421) - ���������Ƴ�Ԫ�أ���ȫ��

## ʹ�ó���

1. **���ݴ���** - ��ȫ�ش�����ּ������ݣ����� null �쳣
2. **Web API** - ���������������Ӧ���ݵļ��ϲ���
3. **����ת��** - ������������ݽṹ��DataTable��Dictionary��ConcurrentDictionary��֮���ת��
4. **��ҳ����** - �Դ����ݼ��Ͻ��з�ҳ����
5. **ͳ�Ʒ���** - �Լ������ݽ�����͡�ƽ��ֵ����ֵ��ͳ�Ƽ���
6. **ҵ���߼�** - ���Ϲ��ˡ����򡢷����ҵ����
7. **��������** - ת��Ϊ�̰߳�ȫ�� ConcurrentDictionary
8. **������ϴ** - ȥ�ء�ȥ�� null ֵ�������������

## ע������

1. ���з���������չ��������Ҫͨ������ʵ������
2. ���� "Safe" ��׺�ķ������� null ֵ�����˰�ȫ���������׳��쳣
3. �󲿷ַ���������Ϊ null ʱ�᷵�ؿռ��ϻ�Ĭ��ֵ���������׳��쳣
4. [Page<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L116-L120) ����ʹ�� 0 ��Ϊ��ʼҳ����
5. [ToDataTable<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L316-L331) �������ڷ���ʵ�֣����ܿ��ܲ����ֶ�ӳ��
6. [Chunk<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumerableExtensions.cs#L252-L267) ����ʹ���ӳ�ִ�У��ڴ�Ч�ʽϸ�
7. ͳ�Ʒ����ڼ���Ϊ��ʱ�᷵��Ĭ��ֵ��ͨ���� 0�����������׳��쳣
8. ת��������ί�в���Ϊ null ʱ��ʹ��Ĭ��ί�У�����ݴ���
9. ���ϲ���������������������������Լ�飬��ߴ��뽡׳��