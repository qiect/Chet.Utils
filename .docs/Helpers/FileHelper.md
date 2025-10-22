# FileHelper �๦���ĵ�

## ����

[FileHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L11-L1054) ��һ����̬�����࣬�ṩ��ȫ����ļ���Ŀ¼�������ܡ����༯�����ļ�������������д���������Թ���Ŀ¼������ѹ����ѹ����ȫУ��ȶ��ֹ��ܣ�ּ�ڼ� .NET �е��ļ�ϵͳ������

## ��Ҫ����ģ��

### 1. �ļ���������

�ṩ�������ļ���Ŀ¼�������ܣ������������ԡ�������ɾ�������ơ��ƶ��ȡ�

**��Ҫ������**
- [FileExists()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L19-L22) - ����ļ��Ƿ����
- [DirectoryExists()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L28-L31) - ���Ŀ¼�Ƿ����
- [CreateDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L37-L42) - ����Ŀ¼
- [DeleteFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L49-L58) - ɾ���ļ�
- [DeleteDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L65-L70) - ɾ��Ŀ¼������������
- [CopyFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L77-L87) - �����ļ�
- [MoveFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L95-L114) - �ƶ��ļ�
- [GetFileInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L120-L123) - ��ȡ�ļ���Ϣ
- [GetDirectoryInfo()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L129-L132) - ��ȡĿ¼��Ϣ
- [GetFileSize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L139-L148) - ��ȡ�ļ���С���ֽڣ�

### 2. �ļ���д����

�ṩ�ı��Ͷ������ļ��Ķ�д���ܣ�֧�ֶ��ֱ����ʽ�Ͱ�ȫд�뷽ʽ��

**��Ҫ������**
- [ReadTextFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L158-L168) - ��ȡ�ı��ļ�����
- [ReadLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L177-L187) - ���ж�ȡ�ı��ļ�����
- [ReadBinaryFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L195-L204) - ��ȡ�������ļ�����
- [WriteTextFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L213-L227) - д���ı����ļ�
- [WriteLines()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L237-L251) - ����д���ı����ļ�
- [WriteBinaryFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L259-L265) - д����������ݵ��ļ�
- [SafeWriteTextFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L274-L291) - ��ȫд���ļ�����д����ʱ�ļ������滻ԭ�ļ���
- [AppendText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L299-L302) - ׷���ı����ļ�
- [ReadPartialFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L311-L322) - ��ȡ�ļ��Ĳ�������
- [ReadFileInChunks()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L330-L348) - ��ʽ��ȡ���ļ�

### 3. �ļ�������Ԫ����

�ṩ�ļ����Ժ�Ԫ���ݵĻ�ȡ�����ù��ܡ�

**��Ҫ������**
- [GetFileCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L357-L360) - ��ȡ�ļ�����ʱ��
- [GetFileLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L367-L370) - ��ȡ�ļ�������ʱ��
- [GetFileLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L377-L380) - ��ȡ�ļ�����޸�ʱ��
- [SetFileCreationTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L387-L390) - �����ļ�����ʱ��
- [SetFileLastAccessTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L397-L400) - �����ļ�������ʱ��
- [SetFileLastWriteTime()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L407-L410) - �����ļ�����޸�ʱ��
- [GetFileAttributes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L416-L419) - ��ȡ�ļ�����
- [SetFileAttributes()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L426-L429) - �����ļ�����
- [GetFileExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L435-L438) - ��ȡ�ļ���չ��
- [GetFileNameWithoutExtension()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L445-L448) - ��ȡ������չ�����ļ���

### 4. Ŀ¼����

�ṩĿ¼��صĲ������ܣ������ļ�����Ŀ¼��ö�١�Ŀ¼��С���㡢���Ƶȡ�

**��Ҫ������**
- [GetFiles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L459-L462) - ��ȡĿ¼�µ������ļ�
- [GetDirectories()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L472-L475) - ��ȡĿ¼�µ�������Ŀ¼
- [GetFileSystemEntries()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L483-L486) - ��ȡĿ¼�µ������ļ�����Ŀ¼
- [GetDirectorySize()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L495-L507) - ����Ŀ¼��С
- [CopyDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L516-L542) - ����Ŀ¼������������
- [ClearDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L550-L574) - ���Ŀ¼����
- [GetParentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L580-L583) - ��ȡ��Ŀ¼·��
- [GetDirectoryName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L589-L592) - ��ȡĿ¼����
- [GetCurrentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L598-L601) - ��ȡ����ǰ����Ŀ¼
- [SetCurrentDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L607-L610) - ���õ�ǰ����Ŀ¼

### 5. �ļ�ѹ�����ѹ

�ṩ�ļ���Ŀ¼��ѹ�����ѹ���ܣ�֧�� GZip �� ZIP ��ʽ��

**��Ҫ������**
- [CompressFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L619-L632) - ѹ���ļ�
- [DecompressFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L641-L655) - ��ѹ�ļ�
- [CompressDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L663-L672) - ѹ��Ŀ¼
- [DecompressDirectory()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L680-L690) - ��ѹĿ¼
- [AddFileToZip()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L699-L706) - ��ZIPѹ��������ļ�
- [ExtractFileFromZip()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L715-L729) - ��ZIPѹ������ȡ�ļ�
- [ListZipEntries()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L737-L750) - �г�ZIPѹ�����е�������Ŀ
- [CompressText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L759-L774) - ѹ���ı�����
- [DecompressText()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L783-L802) - ��ѹ�ı�����
- [CreateZipFromFiles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L810-L823) - ����ZIPѹ��������Ӷ���ļ�

### 6. �ļ���ȫ��У��

�ṩ�ļ���ȫ��������У�鹦�ܣ�������ϣ���㡢�ļ��Ƚϡ����ܽ��ܵȡ�

**��Ҫ������**
- [CalculateMD5()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L832-L844) - �����ļ�MD5��ϣֵ
- [CalculateSHA1()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L852-L864) - �����ļ�SHA1��ϣֵ
- [CalculateSHA256()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L872-L884) - �����ļ�SHA256��ϣֵ
- [CompareFiles()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L892-L906) - �Ƚ������ļ��Ƿ���ͬ
- [EncryptFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L915-L940) - �����ļ�
- [DecryptFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L950-L977) - �����ļ�
- [CalculateCRC32()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L985-L1008) - �����ļ�CRC32У��ֵ
- [HideFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L1025-L1033) - �����ļ�
- [UnhideFile()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L1040-L1048) - ȡ���ļ�����
- [IsReadOnly()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\FileHelper.cs#L1050-L1053) - ����ļ��Ƿ�Ϊֻ��

## ʹ�ó���

1. **�ļ�����** - ��Ӧ�ó����е��ļ���Ŀ¼����
2. **���ݳ־û�** - �ṩ�ı��Ͷ��������ݵĶ�д����
3. **�ļ���ȫ** - ʵ���ļ����ܡ���ϣУ��Ȱ�ȫ����
4. **����ѹ��** - ���ٴ洢�ռ�ʹ������
5. **��������** - ��������ļ���Ŀ¼����
6. **������ͬ��** - ʵ���ļ����ơ��ƶ��ͱȽϹ���

## ע������

1. ���ַ������ļ���Ŀ¼������ʱ���׳��쳣
2. ���ܹ���ʹ�� AES �㷨����Կ�ͳ�ʼ�����������ṩ����������
3. ���ļ���������ʹ����ʽ�������Ա����ڴ�����
4. ѹ���ͽ�ѹ���������� .NET �� GZipStream �� ZipFile ��
5. ��ȫд�빦��ͨ����ʱ�ļ�����ȷ���ļ�д���ԭ����
6. �����ļ����Բ����ڲ�ͬ����ϵͳ�Ͽ����в�ͬ����Ϊ