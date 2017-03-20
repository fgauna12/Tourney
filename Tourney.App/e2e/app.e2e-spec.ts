import { Tourney.AppPage } from './app.po';

describe('tourney.app App', () => {
  let page: Tourney.AppPage;

  beforeEach(() => {
    page = new Tourney.AppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
