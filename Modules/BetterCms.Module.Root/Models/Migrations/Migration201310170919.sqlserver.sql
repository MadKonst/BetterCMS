﻿-- Make lowercase and remove all trailing slashes and spaces.
UPDATE bcms_root.Pages
SET PageUrlHash = substring(lower(master.dbo.fn_varbintohexstr(HashBytes('MD5', lower(replace(replace(rtrim(replace(replace(ltrim(rtrim(PageUrl)), N' ', N'${space}$' ), N'/', N' ' )), N' ', N'/' ), N'${space}$', N' ' ))))),3,32)
FROM bcms_root.Pages
-- Restore root path.
UPDATE bcms_root.Pages
SET PageUrlHash = substring(lower(master.dbo.fn_varbintohexstr(HashBytes('MD5', N'/'))),3,32)
FROM bcms_root.Pages
WHERE PageUrl = N'/'