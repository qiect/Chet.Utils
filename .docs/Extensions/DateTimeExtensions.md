# DateTimeExtensions �๦���ĵ�

## ����

[DateTimeExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L8-L440) ��һ����̬��չ�࣬Ϊ `DateTime` �����ṩ�˷ḻ����չ�������������ʱ���жϡ�ת�������㡢��ʽ���ȶ��ֹ��ܣ�ּ�ڼ�����ʱ��Ĵ����������ߴ���Ŀɶ��Ժͱ����ԡ�

## ��Ҫ����ģ��

### 1. ״̬�жϷ���

�ṩ DateTime ״̬���ı�ݷ�����

**��Ҫ������**
- [IsDefault()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L13-L13) - �ж� DateTime �Ƿ�ΪĬ��ֵ��δ��ʼ����
- [IsMinValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L19-L19) - �ж� DateTime �Ƿ�Ϊ��Сֵ
- [IsMaxValue()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L25-L25) - �ж� DateTime �Ƿ�Ϊ���ֵ
- [IsToday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L31-L31) - �ж� DateTime �Ƿ�Ϊ����
- [IsLeapYear()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L37-L37) - �ж� DateTime �Ƿ�Ϊ����
- [IsWeekend()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L43-L44) - �ж� DateTime �Ƿ�Ϊ��ĩ�����������գ�
- [IsWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L50-L51) - �ж� DateTime �Ƿ�Ϊ�����գ���һ�����壩
- [IsAM()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L398-L398) - �ж� DateTime �Ƿ�Ϊ����
- [IsPM()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L404-L404) - �ж� DateTime �Ƿ�Ϊ����

### 2. ʱ���ת������

�ṩ DateTime �� Unix ʱ���֮����໥ת����

**��Ҫ������**
- [ToUnixTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L60-L61) - DateTime תΪ Unix ʱ������룩
- [ToUnixTimestampMs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L67-L68) - DateTime תΪ Unix ʱ��������룩
- [FromUnixTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L74-L75) - Unix ʱ������룩תΪ DateTime
- [FromUnixTimestampMs()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L81-L82) - Unix ʱ��������룩תΪ DateTime

### 3. �ַ�����ʽ������

�ṩ��������ʱ���ʽ���ַ���ת����

**��Ҫ������**
- [ToFormatString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L89-L90) - DateTime תΪָ����ʽ�ַ���
- [ToIso8601String()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L96-L97) - DateTime תΪ ISO8601 ��ʽ�ַ���
- [ToRfc1123String()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L103-L104) - DateTime תΪ RFC1123 ��ʽ�ַ���
- [ToChineseDateString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L110-L111) - DateTime תΪ���������ַ���
- [ToChineseDateTimeString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L117-L118) - DateTime תΪ��������ʱ���ַ���
- [ToTimestampString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L124-L125) - DateTime תΪʱ����ַ���
- [ToDateString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L131-L132) - DateTime תΪ�����ַ���
- [ToTimeString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L138-L139) - DateTime תΪʱ���ַ���
- [ToShortTimeString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L145-L146) - DateTime תΪ��ʱ���ַ���
- [ToChineseWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L152-L155) - DateTime תΪ��������
- [ToEnglishWeekday()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L161-L162) - DateTime תΪӢ������
- [ToQuarter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L168-L171) - DateTime תΪ����
- [ToChineseLunarDate()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L177-L188) - DateTime תΪũ�������ַ���
- [ToCustomTimestamp()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L329-L330) - DateTime תΪʱ������Զ����ʽ�����ȵ����룩
- [ToFriendlyString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L336-L345) - DateTime תΪ�Ѻ�ʱ������
- [ToMinguoString()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L410-L414) - DateTime תΪ��������ַ���

### 4. ʱ����㷽��

�ṩʱ������������ʱ�����㹦�ܡ�

**��Ҫ������**
- [AddDaysSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L194-L195) - DateTime ����ָ������
- [AddHoursSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L201-L202) - DateTime ����ָ��Сʱ��
- [AddMinutesSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L208-L209) - DateTime ����ָ��������
- [AddSecondsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L215-L216) - DateTime ����ָ������
- [AddMonthsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L222-L223) - DateTime ����ָ���·���
- [AddYearsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L229-L230) - DateTime ����ָ�������
- [DaysBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L236-L237) - ��ȡ���� DateTime ֮���������
- [HoursBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L243-L244) - ��ȡ���� DateTime ֮���Сʱ��
- [MinutesBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L250-L251) - ��ȡ���� DateTime ֮��ķ��Ӳ�
- [SecondsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L257-L258) - ��ȡ���� DateTime ֮���������
- [SpanBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L264-L265) - ��ȡ���� DateTime ֮���ʱ������TimeSpan��

### 5. ʱ��ȽϷ���

�ṩʱ��ȽϺͷ�Χ�жϹ��ܡ�

**��Ҫ������**
- [IsBetween()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L272-L273) - �ж� DateTime �Ƿ���ָ����Χ�ڣ������߽磩
- [IsBefore()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L279-L280) - �ж� DateTime �Ƿ�����ָ��ʱ��
- [IsAfter()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L286-L287) - �ж� DateTime �Ƿ�����ָ��ʱ��

### 6. ʱ��ת������

�ṩʱ��ת�����ܡ�

**��Ҫ������**
- [ToLocalTimeSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L293-L294) - DateTime תΪ����ʱ��
- [ToUtcTimeSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L300-L301) - DateTime תΪ UTC ʱ��
- [ToTimeZone()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L307-L311) - DateTime תΪָ��ʱ��ʱ��

### 7. ������㷽��

�ṩ����������������ϵͳת�����ܡ�

**��Ҫ������**
- [ToAge()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L351-L358) - DateTime תΪ Age�����䣬����ݼ��㣩
- [ToJulianDayNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L420-L439) - DateTime תΪ JDN�������պţ�

### 8. ���ݽṹת������

�ṩ DateTime ���������ݽṹ��ת�����ܡ�

**��Ҫ������**
- [ToWeekdayNumber()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L364-L364) - DateTime תΪ���ڼ�������
- [ToYMD()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L370-L371) - DateTime תΪ������Ԫ��
- [ToHMS()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L377-L378) - DateTime תΪʱ����Ԫ��
- [ToDateOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L384-L384) - DateTime תΪ DateOnly��.NET 6+��
- [ToTimeOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\DateTimeExtensions.cs#L390-L390) - DateTime תΪ TimeOnly��.NET 6+��

## ʹ�ó���

1. **ʱ���ʽ��** - �� DateTime ת��Ϊ���ָ�ʽ���ַ�����ʾ
2. **ʱ�����** - ����ʱ������ʱ��Ȳ���
3. **���ʻ���ʾ** - ֧�����ġ�Ӣ�ĵȶ������Ե�ʱ����ʾ
4. **ʱ������** - ����ͬʱ����ʱ��ת��
5. **�û�����** - �ṩ�Ѻõ�ʱ����ʾ����"�ո�"��"5����ǰ"��
6. **���ݽ���** - ʱ���ת��������ϵͳ�����ݴ���
7. **��������** - ���ɸ��ָ�ʽ������ʱ�䱨��
8. **ҵ���߼�** - �жϹ����ա���ĩ�������ҵ�����ʱ������

## ע������

1. ���з���������չ��������Ҫͨ�� `DateTime` ʵ������
2. �󲿷ַ������Ա߽����������˰�ȫ����
3. ʱ���ת������ DateTimeOffset ʵ�֣�����ʱ����׼ȷ
4. ũ��ת����֧���й�ũ������Ҫ���� System.Globalization.ChineseLunisolarCalendar
5. �Ѻ�ʱ���������ڵ�ǰʱ����㣬���������ʱ����ʾ
6. ������㿼�����·ݺ����ڣ��������ȷ
7. ʱ��ת����Ҫϵͳ֧����Ӧ��ʱ����ʶ
8. ���ַ������� ToDateOnly��ToTimeOnly����Ҫ .NET 6+ �汾֧��