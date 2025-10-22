# FileExtensions �๦���ĵ�

## ����

[FileExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L7-L760) ��һ����̬��չ�࣬Ϊ�ļ�ϵͳ������ͣ�string��FileInfo��DirectoryInfo���ṩ�˷ḻ����չ��������������ļ���д��·��������Ϣ��ȡ�����Բ����ȶ��ֹ��ܣ�ּ�ڼ��ļ�ϵͳ��������ߴ���İ�ȫ�ԺͿɶ��ԣ������ڸ����ļ���������

## ��Ҫ����ģ��

### 1. �ļ�������չ������string ���ͣ�

�ṩ�����ļ�·���ַ������ļ��������ܡ�

#### �ļ�״̬�жϷ���
- [Exists()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L12-L12) - �ж��ļ��Ƿ����

#### �ļ���ȡ����
- [ReadAllText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L18-L19) - ��ȡ�ļ������ı�����
- [ReadAllLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L25-L26) - ��ȡ�ļ�����������
- [ReadAllBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L32-L33) - ��ȡ�ļ������ֽ�����

#### �ļ�д�뷽��
- [WriteAllText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L39-L40) - д���ı����ݵ��ļ������ǣ�
- [WriteAllLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L46-L47) - д��������ݵ��ļ������ǣ�
- [WriteAllBytes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L53-L54) - д���ֽ����ݵ��ļ������ǣ�
- [AppendText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L60-L61) - ׷���ı����ݵ��ļ�
- [AppendLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L67-L68) - ׷�Ӷ������ݵ��ļ�

#### �ļ���������
- [DeleteFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L74-L77) - ɾ���ļ�
- [CopyTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L83-L87) - �����ļ���Ŀ��·��
- [MoveTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L94-L100) - �ƶ��ļ���Ŀ��·��

#### �ļ���Ϣ��ȡ����
- [GetFileSize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L106-L107) - ��ȡ�ļ���С���ֽڣ�
- [GetCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L113-L114) - ��ȡ�ļ�����ʱ��
- [GetLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L120-L121) - ��ȡ�ļ�����޸�ʱ��
- [GetLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L127-L128) - ��ȡ�ļ�������ʱ��

#### �ļ����Բ�������
- [IsReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L134-L135) - �ж��ļ��Ƿ�Ϊֻ��
- [SetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L141-L144) - �����ļ�Ϊֻ��
- [UnsetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L150-L153) - ȡ���ļ�ֻ������
- [IsHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L186-L191) - �ж��ļ��Ƿ�Ϊ�����ļ�
- [SetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L197-L203) - �����ļ�Ϊ�����ļ�
- [UnsetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L209-L215) - ȡ���ļ���������

#### �ļ���ϣֵ����
- [GetFileMd5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L159-L166) - ��ȡ�ļ��� MD5 ֵ
- [GetFileSha256()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L172-L179) - ��ȡ�ļ��� SHA256 ֵ

### 2. ·��������չ������string ���ͣ�

�ṩ·������ͷ������ܡ�

#### ·���жϷ���
- [IsAbsolute()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L225-L226) - �ж�·���Ƿ�Ϊ����·��
- [IsRelative()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L232-L233) - �ж�·���Ƿ�Ϊ���·��
- [IsUncPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L315-L316) - �ж�·���Ƿ�Ϊ UNC ·��
- [IsRootDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L352-L357) - �ж�·���Ƿ�Ϊ��Ŀ¼
- [IsFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L378-L379) - �ж�·���Ƿ�Ϊ�ļ�
- [IsDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L385-L386) - �ж�·���Ƿ�ΪĿ¼
- [ExistsPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L393-L394) - �ж�·�����ļ���Ŀ¼�Ƿ����

#### ·���ֽⷽ��
- [GetExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L240-L241) - ��ȡ�ļ���չ�������㣩
- [GetFileName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L247-L248) - ��ȡ�ļ���������·����
- [GetFileNameWithoutExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L254-L255) - ��ȡ�ļ�����������չ����
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L261-L262) - ��ȡ�ļ�����Ŀ¼·��
- [GetPathRoot()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L275-L276) - ��ȡ·���ĸ�Ŀ¼
- [GetParentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L344-L349) - ��ȡ·���ĸ�Ŀ¼·��
- [SplitDirectories()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L323-L327) - ��ȡ·���ĸ���Ŀ¼���ֽ�Ϊ���飩

#### ·����������
- [CombinePaths()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L268-L269) - �ϲ����·��Ϊһ������·��
- [GetFullPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L282-L283) - ��ȡ·���������淶·��������·����
- [ChangeExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L296-L297) - ����·������չ��
- [NormalizePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L363-L367) - ��ȡ·���Ĺ淶����ʽ
- [GetRelativePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L372-L375) - ��ȡ·�������·��

#### ·����֤����
- [HasInvalidChars()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L303-L308) - �ж�·���Ƿ������Ч�ַ�
- [FileNameHasInvalidChars()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L310-L313) - �ж��ļ����Ƿ������Ч�ַ�

#### ϵͳ·����ȡ����
- [GetTempFilePath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L286-L286) - ��ȡ��ʱ�ļ�·��
- [GetTempDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L289-L289) - ��ȡ��ʱĿ¼·��
- [GetDirectorySeparator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L318-L318) - ��ȡ·���ָ���
- [GetAltDirectorySeparator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L320-L320) - ��ȡ����·���ָ���
- [GetVolumeSeparator()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L322-L322) - ��ȡ·����ָ���

### 3. FileInfo ��չ����

�ṩ���� FileInfo ������ļ��������ܡ�

#### �ļ�״̬�жϷ���
- [ExistsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L403-L403) - �ж��ļ��Ƿ����

#### �ļ���Ϣ��ȡ����
- [GetSize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L409-L409) - ��ȡ�ļ���С���ֽڣ�
- [GetExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L415-L415) - ��ȡ�ļ���չ�������㣩
- [GetFileName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L421-L421) - ��ȡ�ļ���������·����
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L427-L427) - ��ȡ�ļ�����Ŀ¼·��
- [GetCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L433-L433) - ��ȡ�ļ�����ʱ��
- [GetLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L439-L439) - ��ȡ�ļ�����޸�ʱ��
- [GetLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L445-L445) - ��ȡ�ļ�������ʱ��

#### �ļ����Բ�������
- [IsReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L451-L451) - �ж��ļ��Ƿ�Ϊֻ��
- [SetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L458-L461) - �����ļ�Ϊֻ��
- [UnsetReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L467-L470) - ȡ���ļ�ֻ������
- [IsHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L539-L544) - �ж��ļ��Ƿ�Ϊ�����ļ�
- [SetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L550-L553) - �����ļ�Ϊ�����ļ�
- [UnsetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L559-L562) - ȡ���ļ���������

#### �ļ���������
- [DeleteFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L476-L479) - ɾ���ļ�
- [CopyTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L485-L488) - �����ļ���Ŀ��·��
- [MoveTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L495-L501) - �ƶ��ļ���Ŀ��·��

#### �ļ���ϣֵ����
- [GetMd5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L507-L514) - ��ȡ�ļ��� MD5 ֵ
- [GetSha256()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L520-L527) - ��ȡ�ļ��� SHA256 ֵ

### 4. DirectoryInfo ��չ����

�ṩ���� DirectoryInfo �����Ŀ¼�������ܡ�

#### Ŀ¼״̬�жϷ���
- [ExistsSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L571-L571) - �ж�Ŀ¼�Ƿ����
- [IsHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L651-L656) - �ж�Ŀ¼�Ƿ�Ϊ����Ŀ¼

#### Ŀ¼��Ϣ��ȡ����
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L577-L577) - ��ȡĿ¼��
- [GetFullPath()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L583-L583) - ��ȡĿ¼����·��
- [GetCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L589-L589) - ��ȡĿ¼����ʱ��
- [GetLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L595-L595) - ��ȡĿ¼����޸�ʱ��
- [GetLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L601-L601) - ��ȡĿ¼������ʱ��

#### Ŀ¼���ݻ�ȡ����
- [GetFilesSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L608-L609) - ��ȡĿ¼�������ļ����ɵݹ飩
- [GetDirectoriesSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L616-L618) - ��ȡĿ¼��������Ŀ¼���ɵݹ飩
- [GetFilePaths()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L640-L648) - ��ȡĿ¼�������ļ�·�����ɵݹ飩
- [GetDirectoryPaths()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L658-L666) - ��ȡĿ¼��������Ŀ¼·�����ɵݹ飩

#### Ŀ¼��������
- [CreateSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L624-L628) - ����Ŀ¼����������ڣ�
- [DeleteSafe()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L634-L637) - ɾ��Ŀ¼���ɵݹ飩
- [MoveTo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L672-L675) - �ƶ�Ŀ¼��Ŀ��·��
- [SetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L679-L682) - ����Ŀ¼Ϊ����Ŀ¼
- [UnsetHidden()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\FileExtensions.cs#L688-L691) - ȡ��Ŀ¼��������

## ʹ�ó���

1. **�ļ�����** - ��д�ļ��������ƶ��ļ�����ȡ�ļ���Ϣ�Ȳ���
2. **·������** - ·����ϡ��ֽ⡢�淶������
3. **�ļ�ϵͳ����** - ������ɾ�����ƶ�Ŀ¼���ļ�
4. **�ļ���֤** - ����ļ��Ƿ���ڡ���֤·����Ч��
5. **�ļ����Թ���** - �����ļ�ֻ�������ص�����
6. **�ļ�У��** - �����ļ���ϣֵ����������У��
7. **Ŀ¼����** - ��ȡĿ¼�������ļ�����Ŀ¼��Ϣ
8. **��ʱ�ļ�����** - �����͹�����ʱ�ļ���Ŀ¼

## ע������

1. ���з���������չ��������Ҫͨ����Ӧ��ʵ������
2. ���� "Safe" ��׺�ķ������� null ֵ�����˰�ȫ���������׳��쳣
3. �󲿷��ļ���������������ļ��Ƿ���ڣ�������������ڵ��ļ�
4. ·����������֧�ֿ�ƽ̨·���ָ�������
5. �ļ���ϣ���㷽��ʹ�� using ���ȷ����Դ��ȷ�ͷ�
6. �ݹ���������ṩ����ѡ��������Ƶݹ����
7. ���Բ��������ڲ���ǰ�������Ƿ����
8. ����д������������� null ��������˰�ȫ����
9. ·���淶������ʹ�� Uri ����п�ƽ̨���ݴ���