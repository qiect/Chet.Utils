# TaskHelper �๦���ĵ�

## ����

[TaskHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L10-L1234) ��һ����̬�����࣬ר�������첽����Ĺ������ȡ���غͿ��ơ������ṩ�˷ḻ������������ܣ��������񴴽������ƹ������Ȳ��д������ͳ�ơ���ϲ����Լ��߼�����ģʽ�ȣ�ּ�ڼ򻯸��ӵ��첽��̳�����

## ��Ҫ����ģ��

### 1. ���񴴽�������

�ṩ���ַ�ʽ�����������첽����

**��Ҫ������**
- [Run()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L21-L24) - ����������һ���첽����
- [Run<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L34-L37) - ����������һ��������ֵ���첽����
- [RunAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L46-L49) - ����������һ���첽�����첽ί�а汾��
- [RunAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L60-L63) - ����������һ��������ֵ���첽�����첽ί�а汾��
- [Delay()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L71-L74) - ����һ���ӳ�����TimeSpan�汾��
- [Delay()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L82-L85) - ����һ���ӳ����񣨺���汾��
- [CompletedTask()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L91-L94) - ����һ������ɵ�����
- [FromResult<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L102-L105) - ����һ������ɵ����񣨴�����ֵ��
- [FromException()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L112-L115) - ����һ��ʧ�ܵ�����
- [FromException<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L124-L127) - ����һ��ʧ�ܵ����񣨴�����ֵ���ͣ�
- [FromCanceled()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L133-L136) - ����һ����ȡ��������
- [FromCanceled<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L144-L147) - ����һ����ȡ�������񣨴�����ֵ���ͣ�

### 2. ������������

�ṩ����Ŀ��ƺ͹����ܣ������ȴ�����ʱ�����Ժ��۶ϵȡ�

**��Ҫ������**
- [WhenAll()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L157-L160) - �ȴ������������
- [WhenAll<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L168-L171) - �ȴ�����������ɣ����Ͱ汾��
- [WhenAny()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L179-L182) - �ȴ�����һ���������
- [WhenAny<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L191-L194) - �ȴ�����һ��������ɣ����Ͱ汾��
- [WithTimeout()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L202-L217) - ��ʱ����ִ������
- [WithTimeout<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L227-L243) - ��ʱ����ִ�����񣨷��Ͱ汾��
- [RetryAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L253-L271) - �����Ի��Ƶ�����ִ��
- [RetryAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L281-L303) - �����Ի��Ƶ�����ִ�У����Ͱ汾��
- [CircuitBreakerAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L312-L316) - �����۶���ģʽʵ��
- [CircuitBreakerAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L326-L331) - �����۶���ģʽʵ�֣����Ͱ汾��

### 3. ��������벢�д���

�ṩ����ĵ��ȺͲ��д����ܡ�

**��Ҫ������**
- [ParallelForEachAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L342-L374) - ����ִ�ж������
- [ParallelForEachAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L386-L418) - ����ִ�ж�����񣨴�����Դ��
- [ParallelSelectAsync<T, TResult>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L431-L476) - ������ִ������
- [ScheduleTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L486-L529) - ������е�����

### 4. ��������ͳ��

�ṩ����ִ�еļ�غ�ͳ�ƹ��ܡ�

**��Ҫ������**
- [MonitorTaskAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L540-L566) - �������ܼ��
- [MonitorTaskAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L577-L606) - �������ܼ�أ����Ͱ汾��
- [MonitorBatchTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L615-L643) - �����������ܼ��
- [TrackTaskProgress()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L653-L677) - ʵʱ����״̬����

### 5. �����������ʽ����

�ṩ�������Ϻ���ʽ�������ܡ�

**��Ҫ������**
- [ChainTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L686-L693) - ������ʽִ��
- [ChainConditionalTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L702-L713) - ��������������ʽִ��
- [PipelineAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L723-L732) - ������ˮ�ߴ���
- [ForkAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L741-L750) - ����ֲ�ִ��
- [CombineAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L761-L767) - ����ϲ�ִ��
- [RaceAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L777-L784) - ������ִ�У�ֻȡ��һ����ɵģ�

### 6. �߼�����ģʽ

�ṩ�߼�����ģʽ��ʵ�֡�

**��Ҫ������**
- [ProducerConsumerAsync<T>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L796-L806) - ������-������ģʽʵ��
- [CreateTaskPool()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L814-L817) - ����ع���
- [ThrottleAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L826-L857) - �������ִ��
- [ExecutePriorityTasksAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L865-L873) - �������ȼ�����ִ��
- [StateMachineAsync()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L884-L899) - ����״̬��ģʽ

## ����������ݽṹ

### ö������
- [TaskStatus](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L912-L932) - ����״̬ö��

### ���ݽṹ��
- [TaskExecutionInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L937-L959) - ����ִ����Ϣ
- [TaskSchedulerResult](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L964-L981) - ������������
- [TaskPerformanceInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L986-L1013) - ����������Ϣ
- [TaskPerformanceInfo<T>](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1019-L1029) - ����������Ϣ�����Ͱ汾��
- [BatchTaskPerformanceInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1034-L1056) - ��������������Ϣ
- [TaskProgressInfo](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1061-L1083) - ���������Ϣ
- [PriorityTask](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1088-L1100) - ���ȼ�����

### ������
- [CircuitBreaker](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1105-L1192) - �۶���
- [CircuitBreakerOpenException](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1197-L1207) - �۶��������쳣
- [TaskPool](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\TaskHelper.cs#L1212-L1233) - �����

## ʹ�ó���

1. **�첽��̼�** - �򻯸��ӵ��첽���񴴽��͹���
2. **���ܼ��** - �������ִ�����ܺ�״̬
3. **�ݴ���** - ͨ�����Ժ��۶ϻ������ϵͳ�ȶ���
4. **��������** - �������񲢷�ִ������
5. **�������** - ʵ�ָ�������ĵ��Ⱥ�ִ��
6. **���ݴ���ܵ�** - �������ݴ�����ˮ��
7. **������������ģʽ** - ʵ�������������߲���ģʽ
8. **ϵͳ��Դ����** - ͨ�������ȷ�ʽ����ϵͳ��Դ

## ע������

1. �󲿷ַ�����֧��ȡ�����ƣ�CancellationToken������
2. ���д�����֧����󲢷��ȿ���
3. ��ʱ���Ʒ������׳�TimeoutException�쳣
4. ���Ի���֧��ȡ������
5. �۶���ģʽ�����ڷ�ֹ��������
6. ����ؿ�����Ч������Դʹ��
7. ������������ģʽ����System.Threading.Channelsʵ��
8. ���ַ�����Ҫ����ί�л�Lambda���ʽ��Ϊ����