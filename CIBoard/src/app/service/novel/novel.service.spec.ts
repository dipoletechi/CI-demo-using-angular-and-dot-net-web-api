import { TestBed } from '@angular/core/testing';

import { NovelService } from './novel.service';

describe('LoginService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NovelService = TestBed.get(NovelService);
    expect(service).toBeTruthy();
  });
});
