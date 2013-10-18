Objectives
==========
1. Create a simple way for developers to make SQL calls without tripping over human issues such as forgetting to release the connection quickly.
2. Make SQL calls independent of the underlying database. 

Notes
-----
1. Does not use DbProviderFactory because of implementation issues with MySql driver 6.7.4 (too much work on the user's side to register the driver). Own implementation seems to be sufficient for now.
2. Currently only MS SQL and MySql (and MariaDb) are supported.
