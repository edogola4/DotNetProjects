#!/bin/bash

# Push to GitHub script
echo "🚀 Pushing to GitHub..."

# Add remote (update if needed)
git remote add origin https://github.com/edogola4/DotNetProjects.git 2>/dev/null || \
git remote set-url origin https://github.com/edogola4/DotNetProjects.git

# Push all branches
echo "📤 Pushing main branch..."
git push -u origin main

echo "📤 Pushing develop branch..."
git push origin develop

echo "📤 Pushing feature branch..."
git push origin feature/sprint-1-foundation

echo ""
echo "✅ All branches pushed to GitHub!"
echo "🌐 View at: https://github.com/edogola4/DotNetProjects"
