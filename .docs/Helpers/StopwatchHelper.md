# StopwatchHelper �๦���ĵ�

## ����

[StopwatchHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L10-L908) ��һ����̬�����࣬ר���������ܲ����ͼ�ʱ�����������ṩ�˷ḻ�ļ�ʱ���ܣ�����������ʱ�����ִ�в������߾��ȼ�ʱ��������ʱ����ٵȣ�ּ�ڼ����ܲ��Ժʹ����Ż�������

## ��Ҫ����ģ��

### 1. ������ʱ����

�ṩ�򵥵Ĳ���ִ��ʱ��������ܡ�

**��Ҫ������**
- [Time()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L19-L26) - ִ�в��������غ�ʱ�����룩
- [TimePrecise()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L34-L41) - ִ�в��������غ�ʱ���߾���ʱ�����
- [TimeAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L49-L56) - �첽ִ�в��������غ�ʱ�����룩
- [TimePreciseAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L65-L72) - �첽ִ�в��������غ�ʱ���߾���ʱ�����
- [StartNew()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L79-L82) - ����������һ���µļ�ʱ��

### 2. ���ִ�в���

�ṩ���ִ�в�����ͳ�Ʒ������ܡ�

**��Ҫ������**
- [TimeAverage()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L94-L105) - ���ִ�в���������ƽ����ʱ
- [TimeStatistics()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L115-L130) - ���ִ�в�����������ϸͳ����Ϣ
- [TimeAverageAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L139-L151) - �첽���ִ�в���������ƽ����ʱ
- [TimeStatisticsAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L161-L176) - �첽���ִ�в�����������ϸͳ����Ϣ
- [TimeConcurrent()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L185-L202) - ����ִ�в����������ܺ�ʱ
- [TimeConcurrentAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L211-L228) - ����ִ���첽�����������ܺ�ʱ
- [Compare()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L238-L250) - �Ƚ���������������
- [CompareAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L259-L273) - �첽�Ƚ���������������

### 3. �߾��ȼ�ʱ

�ṩ���߾��ȵ�ʱ��������ܡ�

**��Ҫ������**
- [GetTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L288-L291) - ��ȡ�߾���ʱ���
- [TimestampToTimeSpan()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L298-L301) - ��ʱ���ת��Ϊʱ����
- [GetFrequency()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L307-L310) - ��ȡ��ʱ��Ƶ��
- [IsHighResolution()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L317-L320) - ����ʱ���Ƿ���ڸ����ܼ�����
- [TimeHighPrecision()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L328-L336) - ʹ�ø߾��ȼ�ʱ��ִ�в���
- [TimeAverageHighPrecision()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L345-L357) - �߾��ȶ��ִ�в���������ƽ����ʱ
- [MeasureCpuCycles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L365-L373) - ����������CPU������
- [CreateHighPrecisionTimer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L380-L383) - �����߾��ȼ�ʱ��
- [TimeWithCustomTimer()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L391-L395) - ʹ���Զ����ʱ��ִ�в���

### 4. ������ʱ�����

�ṩ�����Լ�ʱ�͸��ٹ��ܡ�

**��Ҫ������**
- [TimeIf()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L407-L415) - ������ִ�м�ʱ
- [TimeAndTrace()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L425-L433) - ������ִ�м�ʱ�����������̨
- [TimeAndLog()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L443-L448) - ��ʱ����¼����־
- [CreateSegmentedStopwatch()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L455-L458) - �����ֶμ�ʱ��
- [TimeWithTimeout()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L467-L475) - ִ�в������ڳ�ʱʱ�׳��쳣
- [TimeWithTimeoutAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L485-L497) - �첽ִ�в������ڳ�ʱʱ�׳��쳣
- [TimeWithRetry()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L507-L546) - ��ʱ�����Բ���ֱ���ɹ���ﵽ����Դ���
- [TimeWithMemoryTracking()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L555-L570) - �����������ڴ����

## ����������ݽṹ

### ���ݽṹ��
- [TimingStatistics](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L580-L629) - ��ʱͳ����Ϣ
- [PerformanceComparison](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L634-L678) - ���ܱȽϽ��
- [CustomTimingResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L706-L728) - �Զ����ʱ���
- [TimeSegment](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L785-L796) - ʱ���
- [RetryResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L855-L877) - ���Խ��
- [MemoryTimingResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L882-L893) - �ڴ��ʱ���

### ������
- [CustomStopwatch](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L683-L729) - �Զ����ʱ��
- [SegmentedStopwatch](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L734-L797) - �ֶμ�ʱ��
- [HighPrecisionTimer](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\StopwatchHelper.cs#L802-L850) - �߾��ȼ�ʱ��

## ʹ�ó���

1. **���ܲ���** - ��������ִ��ʱ�䣬ʶ������ƿ��
2. **�㷨�Ż�** - �Ƚϲ�ͬ�㷨��ִ��Ч��
3. **ϵͳ���** - ��عؼ�������ִ��ʱ��
4. **��׼����** - ���д������ܻ�׼����
5. **�������** - ��ϳ���ִ��ʱ���쳣
6. **�������ܷ���** - �����������������ܱ���
7. **��Դʹ�÷���** - �����������ڴ�ʹ�����
8. **��ʱ����** - Ϊ��������ִ��ʱ������

## ע������

1. ���ַ�����Ҫ����ί�л�Lambda���ʽ��Ϊ����
2. ���ִ�в���������Ҫָ������ĵ�������
3. �߾��ȼ�ʱ����������ϵͳ��ʱ����֧��
4. ����ִ�з�����Ҫע���̰߳�ȫ����
5. �ڴ���ٹ����ṩ���ǽ���ֵ��������GCӰ��
6. ��ʱ���Ʒ������첽�����и�Ϊ��Ч
7. ���Ի��������ڿ�����ʱʧ�ܵĲ���
8. �ֶμ�ʱ�������ڷ������Ӳ������׶κ�ʱ