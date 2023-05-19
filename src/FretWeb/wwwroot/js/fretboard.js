window.addEventListener('DOMContentLoaded', () => {

  function getIndex(e) {
    const index = e.getAttribute('data-fret-index');
    return index ? parseInt(index) : -1;
  }

  function getMaxIndex(fretMap) {
    let maxIndex = 0;
    for (const row of fretMap) {
      for (const column of row) {
        if (column) {
          maxIndex = Math.max(column.i, maxIndex);
        }
      }
    }
    return maxIndex;
  }
  
  function clearHighlights(e) {
    const fretboard = e.closest('.fretboard');
    if (fretboard) {
      fretboard.querySelectorAll('.fret-fade').forEach(f => f.classList.remove('fret-fade'));
      const id = fretboard.id + '-highlight';
      localStorage.removeItem(id);
    }
  }

  function highlight(string, row, column, fretMap) {
    console.log([row, column]);
    const distances = [];
    const elements = [];
    let lastPosition = (row * 100) + column;

    let startColumn = column;
    const maxIndex = getMaxIndex(fretMap);
    for (let i = 1; i <= maxIndex; i++) {
      for (let r = row; r < fretMap.length; r++) {
        for (let c = startColumn; c < fretMap[r].length; c++) {
          const item = fretMap[r][c];
          if (!item) continue;
          if (item.i === i) {
            const position = (r * 100) + c;
            if (position > lastPosition) {
              const rowDistance = r - row;
              const columnDistance = Math.abs(c - column);
              const distance = rowDistance + columnDistance;
              if ((!distances[item.i]) || distances[item.i] > distance) {
                distances[item.i] = distance;
                elements[item.i] = item.e;
                lastPosition = position;
              }
            }
          }
        }
        startColumn = 0;
      }
    }
    string.closest('.fretboard').querySelectorAll('.open-fret-string, .fret-string')
      .forEach(e => {
        if (elements.includes(e) || getIndex(e) === 0) {
          e.classList.remove('fret-fade');
        } else {
          e.classList.add('fret-fade');
        }
      });
  }

  function mapNotes(container, selector, fretMap, fret) {
    const strings = [...container.querySelectorAll(selector)].reverse();
    for (let i = 0; i < strings.length; i++) {
      fretMap[i] ??= [];
      const string = strings[i];
      const index = getIndex(string);
      if (index > -1) {
        fretMap[i][fret] = {e: string, i: index};
        if (index === 0) {
          string.classList.add('fret-btn');
          const id = string.closest('.fretboard').id + '-highlight';
          const json = JSON.stringify({ s: i, f: fret });
          string.addEventListener('click', e => {
            if (e.currentTarget.dataset.highlighted) {
              clearHighlights(e.currentTarget);
              delete e.currentTarget.dataset.highlighted;
              localStorage.removeItem(id);
            } else {
              e.currentTarget.dataset.highlighted = 'true';
              highlight(string, i, fret, fretMap);
              localStorage.setItem(id, json);
            }
          });
        }
      }
    }
  }

  document.querySelectorAll('div.fretboard').forEach(e => {
    const fretMap = [];
    const openFret = e.querySelector('.open-fret');
    mapNotes(openFret, '.open-fret-string', fretMap, 0);
    const frets = [...e.querySelectorAll('.fret')];
    for (let i = 0; i < frets.length; i++) {
      mapNotes(frets[i], '.fret-string', fretMap, i + 1);
    }
    const id = e.id + '-highlight';
    const json = localStorage.getItem(id);
    if (json && json.length > 0) {
      const reference = JSON.parse(json);
      if (typeof reference.s === 'number' && typeof reference.f === 'number') {
        if (reference.f === 0) {
          const openFretStrings = [...openFret.querySelectorAll('.open-fret-string')].reverse();
          const string = openFretStrings[reference.s];
          highlight(string, reference.s, reference.f, fretMap);
          string.dataset.highlighted = 'true';
        } else {
          const fret = frets[reference.f - 1];
          const fretStrings = [...fret.querySelectorAll('.fret-string')].reverse();
          const string = fretStrings[reference.s];
          highlight(string, reference.s, reference.f, fretMap);
          string.dataset.highlighted = 'true';
        }
      }
        
    }
  });
});