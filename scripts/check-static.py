from pathlib import Path
import re
import sys
import xml.etree.ElementTree as ET

root = Path(__file__).resolve().parents[1] / 'CleanKwat'
errors = []

# XML/XAML syntax
for path in root.rglob('*.xaml'):
    try:
        ET.parse(path)
    except Exception as exc:
        errors.append(f'XML invalide: {path}: {exc}')

# StaticResource keys
keys = set()
references = []
for path in root.rglob('*.xaml'):
    text = path.read_text(encoding='utf-8')
    keys.update(re.findall(r'x:Key="([^"]+)"', text))
    references.extend((path, k) for k in re.findall(r'\{StaticResource\s+([^}\s,]+)', text))
for path, key in references:
    if key not in keys:
        errors.append(f'StaticResource introuvable: {key} dans {path}')

# Visible Buttons should have an action
maui = '{http://schemas.microsoft.com/dotnet/2021/maui}'
for path in (root / 'Views').rglob('*.xaml'):
    page = ET.parse(path).getroot()
    for button in page.iter(maui + 'Button'):
        if 'Command' not in button.attrib and 'Clicked' not in button.attrib:
            errors.append(f'Bouton sans action: {path}: {button.attrib.get("Text", "(sans texte)")}')

if errors:
    print('\n'.join(errors))
    sys.exit(1)

print('Validation statique OK: XAML, StaticResource et boutons visibles.')
