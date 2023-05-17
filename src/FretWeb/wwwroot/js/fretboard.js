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
          string.addEventListener('click', e => {
            highlight(string, i, fret, fretMap);
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
  });
});