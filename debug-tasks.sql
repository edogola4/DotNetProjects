-- Debug script to check task data
-- Run this in your PostgreSQL database to verify tasks are being saved

-- Check if there are any users
SELECT 'Users Count' as table_name, COUNT(*) as count FROM "Users";

-- Check if there are any tasks
SELECT 'Tasks Count' as table_name, COUNT(*) as count FROM "Tasks";

-- Show all tasks with user info
SELECT 
    t."Id" as task_id,
    t."Title",
    t."IsCompleted",
    t."UserId",
    u."Username",
    t."CreatedAt"
FROM "Tasks" t
LEFT JOIN "Users" u ON t."UserId" = u."Id"
ORDER BY t."CreatedAt" DESC;

-- Show task counts by user
SELECT 
    u."Username",
    u."Id" as user_id,
    COUNT(t."Id") as total_tasks,
    SUM(CASE WHEN t."IsCompleted" = true THEN 1 ELSE 0 END) as completed_tasks,
    SUM(CASE WHEN t."IsCompleted" = false THEN 1 ELSE 0 END) as pending_tasks
FROM "Users" u
LEFT JOIN "Tasks" t ON u."Id" = t."UserId"
GROUP BY u."Id", u."Username"
ORDER BY u."Username";