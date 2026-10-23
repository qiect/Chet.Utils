const fs = require('fs');
const path = require('path');

function fixMdxFile(filePath) {
  let content = fs.readFileSync(filePath, 'utf8');
  const lines = content.split('\n');
  let inCodeBlock = false;
  let inFrontmatter = false;
  let frontmatterEndFound = false;
  let modified = false;

  for (let i = 0; i < lines.length; i++) {
    let line = lines[i];

    if (!frontmatterEndFound && i === 0 && line.trim() === '---') {
      inFrontmatter = true;
      continue;
    }
    if (inFrontmatter && line.trim() === '---') {
      inFrontmatter = false;
      frontmatterEndFound = true;
      continue;
    }
    if (inFrontmatter) continue;

    if (line.trimStart().startsWith('```')) {
      inCodeBlock = !inCodeBlock;
      continue;
    }
    if (inCodeBlock) continue;

    const originalLine = line;
    let newLine = '';
    let inInlineCode = false;

    for (let j = 0; j < line.length; j++) {
      const ch = line[j];

      if (ch === '`') {
        inInlineCode = !inInlineCode;
        newLine += ch;
        continue;
      }

      if (!inInlineCode && ch === '<') {
        let typeName = '';
        let k = j + 1;
        while (k < line.length && /[A-Za-z0-9_]/.test(line[k])) {
          typeName += line[k];
          k++;
        }
        if (typeName.length > 0 && typeName[0] === typeName[0].toUpperCase() && k < line.length && line[k] === '>') {
          newLine += '\\<' + typeName + '\\>';
          j = k;
          continue;
        }
      }

      if (!inInlineCode && ch === '\\' && j + 1 < line.length && line[j + 1] === '<') {
        let typeName = '';
        let k = j + 2;
        while (k < line.length && /[A-Za-z0-9_]/.test(line[k])) {
          typeName += line[k];
          k++;
        }
        if (typeName.length > 0 && typeName[0] === typeName[0].toUpperCase() && k < line.length && line[k] === '\\' && k + 1 < line.length && line[k + 1] === '>') {
          newLine += '<' + typeName + '>';
          j = k + 1;
          continue;
        }
      }

      newLine += ch;
    }

    if (newLine !== originalLine) {
      lines[i] = newLine;
      modified = true;
    }
  }

  if (modified) {
    fs.writeFileSync(filePath, lines.join('\n'), 'utf8');
    console.log(`Fixed: ${filePath}`);
  }
  return modified;
}

function walkDir(dir) {
  const entries = fs.readdirSync(dir, { withFileTypes: true });
  for (const entry of entries) {
    const fullPath = path.join(dir, entry.name);
    if (entry.isDirectory()) {
      walkDir(fullPath);
    } else if (entry.name.endsWith('.mdx')) {
      fixMdxFile(fullPath);
    }
  }
}

const docsDir = path.join(__dirname, 'docs', 'src', 'content', 'docs');
walkDir(docsDir);
console.log('All done!');